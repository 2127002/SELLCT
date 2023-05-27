/// Base64�ɂ��AES�Í������g�����Z�[�u�f�[�^�̊Ǘ��ł�
/// 
/// �Q�l�T�C�g
/// AES�Í� https://qiita.com/kz-rv04/items/62a56bd4cd149e36ca70
/// Base64  https://docs.oracle.com/javase/jp/8/docs/api/java/util/Base64.html�@Java�ł�������Ă邱�Ƃ͓����ł�
/// 
/// C#�̃��C�u�����Ƃ���LitJson���g���Ă܂�
///
/// �ݒ�f�[�^�̕��͘M���Ă����v�Ȃ悤�ɈÍ������Ă���܂���

using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using Newtonsoft.Json;
using LitJson;
using System.Collections.Generic;

public class DataManager : MonoBehaviour
{
    //�Í����͂䂭�䂭�͊O���֓����Ă��������Ƃ���
    private static readonly string EncryptKey = "qpyky6kdn3yuvd9975w8ar6ackpdg7jj";
    private static readonly int EncryptPasswordCount = 16;
    private static readonly string PasswordChars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private static readonly int PasswordCharsLength = PasswordChars.Length;

    public static ConfigData configData;
    public static SaveData saveData;

    static bool ResetFlag = false;

    public static bool _nowLoadConfigData = false;

    public static bool NowLoadConfigData
    {
        get { return _nowLoadConfigData; }
    }

    private static Dictionary<Language, LanguageData> _languageDataList = new();

    void Awake()
    {
        LoadConfigData();
        if (!NowLoadConfigData)
        {
            SaveConfigData();
        }

        LoadSaveData();

        //����f�[�^�͎g�p���Ȃ��̂ŕs�v
        //LoadLanguageData();
    }

    void OnApplicationQuit()
    {
        if (!ResetFlag)
        {
            SaveConfigData();
            SaveSaveData();
        }
    }

    /// <summary>
    /// �ݒ�f�[�^���Z�[�u���܂�
    /// </summary>
    public static void SaveConfigData()
    {
        //���[�h���Ă��Ȃ��Ȃ�
        if (!_nowLoadConfigData)
        {
            Debug.LogWarning("�f�[�^�����[�h����Ă��Ȃ����߁A�����f�[�^�ŕۑ����܂�");
            configData = new ConfigData();
        }

        //�V���A���C�Y�p�ݒ�f�[�^

        //�p�^�[��A
        JsonSerializerSettings settings = new()
        {
            Formatting = Formatting.Indented,
        };

        //�p�^�[��B
        //LitJson.JsonWriter jwriter = new LitJson.JsonWriter();
        //jwriter.PrettyPrint = true;
        //jwriter.IndentValue = 4;

        //�V���A���C�Y���s

        //�p�^�[��A
        string dataString = JsonConvert.SerializeObject(configData, settings);

        //�p�^�[��B
        //JsonMapper.ToJson(configData,jwriter);
        //string dataString = jwriter.ToString();

        //�t�@�C���p�X������
#if UNITY_EDITOR
        string path = Directory.GetCurrentDirectory() + "\\Assets\\Resources";
#else
        string path = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
#endif

        //�t�@�C�������邩�`�F�b�N
        if (!File.Exists(path + "/config.txt"))
        {
            Debug.LogError("�ݒ�t�@�C�������݂��Ȃ����߁A�������܂�");
            File.Create(path + "/config.txt").Close();
        }

        //�ۑ�
        using StreamWriter writer = new(path + "/config.txt", false);
        writer.WriteLine(dataString);
        writer.Flush();
    }

    /// <summary>
    /// �ݒ�t�@�C�������[�h���܂�
    /// </summary>
    public static void LoadConfigData()
    {
        //�f�[�^������
        configData = new ConfigData();

        //�t�@�C���p�X������
#if UNITY_EDITOR
        string path = Directory.GetCurrentDirectory() + "\\Assets\\Resources";
#else
        string path = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
#endif

        //�t�@�C�������邩�`�F�b�N
        if (!File.Exists(path + "/config.txt"))
        {
            Debug.LogWarning("�ݒ�t�@�C�������݂��Ȃ����߁A�������܂�");
            File.Create(path + "/config.txt").Close();
            _nowLoadConfigData = false;
            configData.IsReset = true;
            return;
        }

        string dataString;

        using (StreamReader reader = new(path + "/config.txt", false))
        {
            dataString = reader.ReadToEnd();
        }

        //Json����ǂݍ���

        //�p�^�[��A
        //configData = JsonUtility.FromJson<ConfigData>(dataString);

        //�p�^�[��B(LitJson)
        configData = JsonMapper.ToObject<ConfigData>(dataString);

        //�������enum�ɕϊ�(��O����)
        try
        {
            configData.language = (Language)Enum.Parse(typeof(Language), configData.languageString);
            _nowLoadConfigData = true;
        }
        catch (Exception)
        {
            Debug.LogError("�ݒ�t�@�C���̃��[�h�Ɏ��s���܂���");
            configData.language = Language.Unknown;
        }
    }

    /// <summary>
    /// �ݒ�f�[�^�̃t�@�C�������݂��邩��Ԃ��܂�
    /// </summary>
    public static bool HasConfigDataFile()
    {

        //�t�@�C���p�X������
#if UNITY_EDITOR
        string path = Directory.GetCurrentDirectory() + "\\Assets\\Resources";
#else
        string path = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
#endif

        //�t�@�C�������邩�`�F�b�N
        return File.Exists(path + "/config.txt");
    }

    /// <summary>
    /// �ݒ�f�[�^�����[�h����Ă��邩��Ԃ��܂�
    /// </summary>
    public static bool HasLoadingConfigData()
    {
        return configData != null;
    }


    /// <summary>
    /// �Z�[�u�f�[�^���Í������ăZ�[�u���܂�
    /// </summary>
    public static void SaveSaveData()
    {

        //���[�h���Ă��Ȃ��Ȃ�
        if (saveData == null)
        {
            Debug.LogWarning("�f�[�^�����[�h����Ă��Ȃ����߁A�����f�[�^�ŕۑ����܂�");
            saveData = new SaveData();
        }

        //�t�@�C���p�X���擾
#if UNITY_EDITOR
        string path = Directory.GetCurrentDirectory() + "\\Assets\\Resources";
#else
        string path = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
#endif

        if (!configData.dataFilePath.Equals("default"))
        {
            path = configData.dataFilePath;
        }

        //�t�@�C�������邩�`�F�b�N
        if (!File.Exists(path + "/save.dat"))
        {
            Debug.LogWarning("�Z�[�u�f�[�^�����݂��Ȃ����߁A�������܂�");
            File.Create(path + "/save.dat").Close();
        }

        string json = JsonMapper.ToJson(saveData);

        //Base64�ňÍ���
        EncryptAesBase64(json, out string iv, out string base64);

        // �ۑ�
        byte[] ivBytes = Encoding.UTF8.GetBytes(iv);
        byte[] base64Bytes = Encoding.UTF8.GetBytes(base64);
        using FileStream file = new(path + "/save.dat", FileMode.Create, FileAccess.Write);
        using BinaryWriter binary = new(file);
        //4�o�C�g����������
        binary.Write(ivBytes.Length);
        binary.Write(ivBytes);

        binary.Write(base64Bytes.Length);
        binary.Write(base64Bytes);
    }

    /// <summary>
    /// �Z�[�u�f�[�^�𕜍����ă��[�h���܂�
    /// </summary>
    public static void LoadSaveData()
    {
        saveData = new SaveData();

        //�t�@�C���p�X���擾
#if UNITY_EDITOR
        string path = Directory.GetCurrentDirectory() + "\\Assets\\Resources";
#else
        string path = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
#endif

        if (!configData.dataFilePath.Equals("default"))
        {
            path = configData.dataFilePath;
        }

        //�t�@�C�������邩�`�F�b�N
        if (!File.Exists(path + "/save.dat"))
        {
            Debug.LogWarning("�Z�[�u�f�[�^�����݂��Ȃ����߁A�������܂�");
            File.Create(path + "/save.dat").Close();
            return;
        }

        //�Í����p�ϐ�
        byte[] ivBytes = null;
        byte[] base64Bytes = null;

        //�t�@�C�����ÓT�I�ȕ��@�ŊJ��(Resources.Load�̓Z�[�u�f�[�^�Ǘ��ɂ͕s�����Ȃ̂�)
        using (FileStream file = new(path + "/save.dat", FileMode.Open, FileAccess.Read))
        using (BinaryReader binary = new(file))
        {
            //4�o�C�g���ǂݍ���
            int length = binary.ReadInt32();
            ivBytes = binary.ReadBytes(length);

            length = binary.ReadInt32();
            base64Bytes = binary.ReadBytes(length);
        }

        string iv = Encoding.UTF8.GetString(ivBytes);
        string base64 = Encoding.UTF8.GetString(base64Bytes);
        //Base64�ŕ�����
        DecryptAesBase64(base64, iv, out string json);

        // �Z�[�u�f�[�^����
        saveData = JsonMapper.ToObject<SaveData>(json);

    }

    /// <summary>
    /// �Z�[�u�f�[�^�̃t�@�C�������݂��邩��Ԃ��܂�
    /// </summary>
    public static bool HasSaveDataFile()
    {
        //�t�@�C���p�X������
#if UNITY_EDITOR
        string path = Directory.GetCurrentDirectory() + "\\Assets\\Resources";
#else
        string path = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
#endif

        //�t�@�C�������邩�`�F�b�N
        return File.Exists(path + "/save.dat");
    }

    /// <summary>
    /// �Z�[�u�f�[�^�����[�h����Ă��邩��Ԃ��܂�
    /// </summary>
    public static bool HasLoadingSaveData()
    {
        return saveData != null;
    }

    /// <summary>
    /// �Z�[�u�f�[�^������������
    /// ���Ăяo����̓Q�[���I���𐄏�
    /// </summary>
    public static void ResetSaveData()
    {
        //�t�@�C���p�X���擾
#if UNITY_EDITOR
        string path = Directory.GetCurrentDirectory() + "\\Assets\\Resources";
#else
            string path = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
#endif
        if (!configData.dataFilePath.Equals("default"))
        {
            path = configData.dataFilePath;
        }

        File.Delete(path + "/save.dat");
        File.Delete(path + "/config.txt");

        ResetFlag = true;
    }

    /// <summary>
    /// �S�Ă̌���t�@�C����ǂݍ��݂܂�
    /// </summary>
    public static void LoadLanguageData()
    {

        //�C���X�^���X����
        _languageDataList = new Dictionary<Language, LanguageData>();

        foreach (Language language in Enum.GetValues(typeof(Language)))
        {

            // Unknown�̓X�L�b�v
            if (language.Equals(Language.Unknown)) continue;

            // Enum�̕�������擾����
            string lanName = Enum.GetName(typeof(Language), language);

            //�t�@�C���p�X���擾
#if UNITY_EDITOR
            string path = Directory.GetCurrentDirectory() + "\\Assets\\Resources";
#else
            string path = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
#endif
            if (!configData.dataFilePath.Equals("default"))
            {
                path = configData.dataFilePath;
            }

            //�t�@�C�������邩�`�F�b�N
            if (!File.Exists(path + "/lang." + lanName))
            {
                Debug.LogWarning("����t�@�C�������݂��Ȃ����߁A�������܂�");
                File.Create(path + "/lang." + lanName).Close();
                continue;
            }

            //�Í����p�ϐ�
            byte[] ivBytes = null;
            byte[] base64Bytes = null;

            //�t�@�C�����ÓT�I�ȕ��@�ŊJ��(Resources.Load�̓Z�[�u�f�[�^�Ǘ��ɂ͕s�����Ȃ̂�)
            using (FileStream file = new(path + "/lang." + lanName, FileMode.Open, FileAccess.Read))
            using (BinaryReader binary = new(file))
            {
                //4�o�C�g���ǂݍ���
                int length = binary.ReadInt32();
                ivBytes = binary.ReadBytes(length);

                length = binary.ReadInt32();
                base64Bytes = binary.ReadBytes(length);
            }

            string iv = Encoding.UTF8.GetString(ivBytes);
            string base64 = Encoding.UTF8.GetString(base64Bytes);
            //Base64�ŕ�����
            DecryptAesBase64(base64, iv, out string json);

            // Json���猾��f�[�^�𐶐�
            _languageDataList.Add(language, JsonMapper.ToObject<LanguageData>(json));

        }


    }

    /// <summary>
    /// ����t�@�C�����쐬���܂�
    /// </summary>
    /// <param name="language">����̎��</param>
    /// <param name="data">����f�[�^</param>
    public static void SaveLanguageData(Language language, LanguageData data)
    {

        // Unknown�̓X�L�b�v
        if (language.Equals(Language.Unknown))
        {
            Debug.LogWarning("Unknown�̌���f�[�^�͐����ł��܂���");
            return;
        }

        // Enum�̕�������擾����
        string lanName = Enum.GetName(typeof(Language), language);

        //�t�@�C���p�X���擾
#if UNITY_EDITOR
        string path = Directory.GetCurrentDirectory() + "\\Assets\\Resources";
#else
            string path = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
#endif
        if (!configData.dataFilePath.Equals("default"))
        {
            path = configData.dataFilePath;
        }

        //�t�@�C�������邩�`�F�b�N
        if (!File.Exists(path + "/lang." + lanName))
        {
            Debug.LogWarning("����t�@�C�������݂��Ȃ����߁A�������܂�");
            File.Create(path + "/lang." + lanName).Close();
        }

        string json = JsonMapper.ToJson(data);

        //Base64�ňÍ���
        EncryptAesBase64(json, out string iv, out string base64);

        // �ۑ�
        byte[] ivBytes = Encoding.UTF8.GetBytes(iv);
        byte[] base64Bytes = Encoding.UTF8.GetBytes(base64);
        using FileStream file = new(path + "/lang." + lanName, FileMode.Create, FileAccess.Write);
        using BinaryWriter binary = new(file);
        //4�o�C�g����������
        binary.Write(ivBytes.Length);
        binary.Write(ivBytes);

        binary.Write(base64Bytes.Length);
        binary.Write(base64Bytes);
    }

    /// <summary>
    /// ���݂̐ݒ�ň����Ă��錾��t�@�C�����擾����
    /// </summary>
    public static LanguageData LanguageData
    {
        get
        {
            if (_languageDataList == null)
            {
                Debug.LogError("����t�@�C�������[�h�����O�ɁA����f�[�^���擾����܂���");
                return null;
            }
            if (configData.language == Language.Unknown)
            {
                Debug.LogError("�ݒ�t�@�C��������������Ă��邽�߁A�������{��ɐݒ肵�܂�");
                configData.language = Language.JP;
            }
            return _languageDataList[configData.language];
        }
    }

    /*�ȉ��ҏW�͔񐄏�*/

    /// <summary>
    /// AES�Í���(Base64�`��)
    /// </summary>
    public static void EncryptAesBase64(string json, out string iv, out string base64)
    {
        byte[] src = Encoding.UTF8.GetBytes(json);
        EncryptAes(src, out iv, out byte[] dst);
        base64 = Convert.ToBase64String(dst);
    }

    /// <summary>
    /// AES������(Base64�`��)
    /// </summary>
    public static void DecryptAesBase64(string base64, string iv, out string json)
    {
        byte[] src = Convert.FromBase64String(base64);
        DecryptAes(src, iv, out byte[] dst);
        json = Encoding.UTF8.GetString(dst).Trim('\0');
    }

    /// <summary>
    /// AES�Í���
    /// </summary>
    public static void EncryptAes(byte[] src, out string iv, out byte[] dst)
    {
        iv = CreatePassword(EncryptPasswordCount);
        dst = null;
        using RijndaelManaged rijndael = new();
        rijndael.Padding = PaddingMode.PKCS7;
        rijndael.Mode = CipherMode.CBC;
        rijndael.KeySize = 256;
        rijndael.BlockSize = 128;

        byte[] key = Encoding.UTF8.GetBytes(EncryptKey);
        byte[] vec = Encoding.UTF8.GetBytes(iv);

        using ICryptoTransform encryptor = rijndael.CreateEncryptor(key, vec);
        using MemoryStream ms = new();
        using CryptoStream cs = new(ms, encryptor, CryptoStreamMode.Write);
        cs.Write(src, 0, src.Length);
        cs.FlushFinalBlock();
        dst = ms.ToArray();
    }

    /// <summary>
    /// AES������
    /// </summary>
    public static void DecryptAes(byte[] src, string iv, out byte[] dst)
    {
        dst = new byte[src.Length];
        using RijndaelManaged rijndael = new();
        rijndael.Padding = PaddingMode.PKCS7;
        rijndael.Mode = CipherMode.CBC;
        rijndael.KeySize = 256;
        rijndael.BlockSize = 128;

        byte[] key = Encoding.UTF8.GetBytes(EncryptKey);
        byte[] vec = Encoding.UTF8.GetBytes(iv);

        using ICryptoTransform decryptor = rijndael.CreateDecryptor(key, vec);
        using MemoryStream ms = new(src);
        using CryptoStream cs = new(ms, decryptor, CryptoStreamMode.Read);
        cs.Read(dst, 0, dst.Length);
    }

    /// <summary>
    /// �p�X���[�h����
    /// </summary>
    /// <param name="count">������</param>
    /// <returns>�p�X���[�h</returns>
    public static string CreatePassword(int count)
    {
        StringBuilder sb = new(count);
        for (int i = count - 1; i >= 0; i--)
        {
            char c = PasswordChars[UnityEngine.Random.Range(0, PasswordCharsLength)];
            sb.Append(c);
        }
        return sb.ToString();
    }
}
