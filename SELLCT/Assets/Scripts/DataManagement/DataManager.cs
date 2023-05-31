/// Base64によるAES暗号化を使ったセーブデータの管理です
/// 
/// 参考サイト
/// AES暗号 https://qiita.com/kz-rv04/items/62a56bd4cd149e36ca70
/// Base64  https://docs.oracle.com/javase/jp/8/docs/api/java/util/Base64.html　Javaですがやってることは同じです
/// 
/// C#のライブラリとしてLitJsonを使ってます
///
/// 設定データの方は弄っても大丈夫なように暗号化してありません

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
    //暗号鍵はゆくゆくは外部へ逃しておきたいところ
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

        //言語データは使用しないので不要
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
    /// 設定データをセーブします
    /// </summary>
    public static void SaveConfigData()
    {
        //ロードしていないなら
        if (!_nowLoadConfigData)
        {
            Debug.LogWarning("データがロードされていないため、初期データで保存します");
            configData = new ConfigData();
        }

        //シリアライズ用設定データ

        //パターンA
        JsonSerializerSettings settings = new()
        {
            Formatting = Formatting.Indented,
        };

        //パターンB
        //LitJson.JsonWriter jwriter = new LitJson.JsonWriter();
        //jwriter.PrettyPrint = true;
        //jwriter.IndentValue = 4;

        //シリアライズ実行

        //パターンA
        string dataString = JsonConvert.SerializeObject(configData, settings);

        //パターンB
        //JsonMapper.ToJson(configData,jwriter);
        //string dataString = jwriter.ToString();

        //ファイルパスを決定
#if UNITY_EDITOR
        string path = Directory.GetCurrentDirectory() + "\\Assets\\Resources";
#else
        string path = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
#endif

        //ファイルがあるかチェック
        if (!File.Exists(path + "/config.txt"))
        {
            Debug.LogError("設定ファイルが存在しないため、生成します");
            File.Create(path + "/config.txt").Close();
        }

        //保存
        using StreamWriter writer = new(path + "/config.txt", false);
        writer.WriteLine(dataString);
        writer.Flush();
    }

    /// <summary>
    /// 設定ファイルをロードします
    /// </summary>
    public static void LoadConfigData()
    {
        //データ初期化
        configData = new ConfigData();

        //ファイルパスを決定
#if UNITY_EDITOR
        string path = Directory.GetCurrentDirectory() + "\\Assets\\Resources";
#else
        string path = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
#endif

        //ファイルがあるかチェック
        if (!File.Exists(path + "/config.txt"))
        {
            Debug.LogWarning("設定ファイルが存在しないため、生成します");
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

        //Jsonから読み込み

        //パターンA
        //configData = JsonUtility.FromJson<ConfigData>(dataString);

        //パターンB(LitJson)
        configData = JsonMapper.ToObject<ConfigData>(dataString);

        //文字列をenumに変換(例外処理)
        try
        {
            configData.language = (Language)Enum.Parse(typeof(Language), configData.languageString);
            _nowLoadConfigData = true;
        }
        catch (Exception)
        {
            Debug.LogError("設定ファイルのロードに失敗しました");
            configData.language = Language.Unknown;
        }
    }

    /// <summary>
    /// 設定データのファイルが存在するかを返します
    /// </summary>
    public static bool HasConfigDataFile()
    {

        //ファイルパスを決定
#if UNITY_EDITOR
        string path = Directory.GetCurrentDirectory() + "\\Assets\\Resources";
#else
        string path = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
#endif

        //ファイルがあるかチェック
        return File.Exists(path + "/config.txt");
    }

    /// <summary>
    /// 設定データがロードされているかを返します
    /// </summary>
    public static bool HasLoadingConfigData()
    {
        return configData != null;
    }


    /// <summary>
    /// セーブデータを暗号化してセーブします
    /// </summary>
    public static void SaveSaveData()
    {

        //ロードしていないなら
        if (saveData == null)
        {
            Debug.LogWarning("データがロードされていないため、初期データで保存します");
            saveData = new SaveData();
        }

        //ファイルパスを取得
#if UNITY_EDITOR
        string path = Directory.GetCurrentDirectory() + "\\Assets\\Resources";
#else
        string path = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
#endif

        if (!configData.dataFilePath.Equals("default"))
        {
            path = configData.dataFilePath;
        }

        //ファイルがあるかチェック
        if (!File.Exists(path + "/save.dat"))
        {
            Debug.LogWarning("セーブデータが存在しないため、生成します");
            File.Create(path + "/save.dat").Close();
        }

        string json = JsonMapper.ToJson(saveData);

        //Base64で暗号化
        EncryptAesBase64(json, out string iv, out string base64);

        // 保存
        byte[] ivBytes = Encoding.UTF8.GetBytes(iv);
        byte[] base64Bytes = Encoding.UTF8.GetBytes(base64);
        using FileStream file = new(path + "/save.dat", FileMode.Create, FileAccess.Write);
        using BinaryWriter binary = new(file);
        //4バイトずつ書き込み
        binary.Write(ivBytes.Length);
        binary.Write(ivBytes);

        binary.Write(base64Bytes.Length);
        binary.Write(base64Bytes);
    }

    /// <summary>
    /// セーブデータを復号してロードします
    /// </summary>
    public static void LoadSaveData()
    {
        saveData = new SaveData();

        //ファイルパスを取得
#if UNITY_EDITOR
        string path = Directory.GetCurrentDirectory() + "\\Assets\\Resources";
#else
        string path = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
#endif

        if (!configData.dataFilePath.Equals("default"))
        {
            path = configData.dataFilePath;
        }

        //ファイルがあるかチェック
        if (!File.Exists(path + "/save.dat"))
        {
            Debug.LogWarning("セーブデータが存在しないため、生成します");
            File.Create(path + "/save.dat").Close();
            return;
        }

        //暗号化用変数
        byte[] ivBytes = null;
        byte[] base64Bytes = null;

        //ファイルを古典的な方法で開く(Resources.Loadはセーブデータ管理には不向きなので)
        using (FileStream file = new(path + "/save.dat", FileMode.Open, FileAccess.Read))
        using (BinaryReader binary = new(file))
        {
            //4バイトずつ読み込む
            int length = binary.ReadInt32();
            ivBytes = binary.ReadBytes(length);

            length = binary.ReadInt32();
            base64Bytes = binary.ReadBytes(length);
        }

        string iv = Encoding.UTF8.GetString(ivBytes);
        string base64 = Encoding.UTF8.GetString(base64Bytes);
        //Base64で復号化
        DecryptAesBase64(base64, iv, out string json);

        // セーブデータ復元
        saveData = JsonMapper.ToObject<SaveData>(json);

    }

    /// <summary>
    /// セーブデータのファイルが存在するかを返します
    /// </summary>
    public static bool HasSaveDataFile()
    {
        //ファイルパスを決定
#if UNITY_EDITOR
        string path = Directory.GetCurrentDirectory() + "\\Assets\\Resources";
#else
        string path = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
#endif

        //ファイルがあるかチェック
        return File.Exists(path + "/save.dat");
    }

    /// <summary>
    /// セーブデータがロードされているかを返します
    /// </summary>
    public static bool HasLoadingSaveData()
    {
        return saveData != null;
    }

    /// <summary>
    /// セーブデータを初期化する
    /// ※呼び出し後はゲーム終了を推奨
    /// </summary>
    public static void ResetSaveData()
    {
        //ファイルパスを取得
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
    /// 全ての言語ファイルを読み込みます
    /// </summary>
    public static void LoadLanguageData()
    {

        //インスタンス生成
        _languageDataList = new Dictionary<Language, LanguageData>();

        foreach (Language language in Enum.GetValues(typeof(Language)))
        {

            // Unknownはスキップ
            if (language.Equals(Language.Unknown)) continue;

            // Enumの文字列を取得する
            string lanName = Enum.GetName(typeof(Language), language);

            //ファイルパスを取得
#if UNITY_EDITOR
            string path = Directory.GetCurrentDirectory() + "\\Assets\\Resources";
#else
            string path = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
#endif
            if (!configData.dataFilePath.Equals("default"))
            {
                path = configData.dataFilePath;
            }

            //ファイルがあるかチェック
            if (!File.Exists(path + "/lang." + lanName))
            {
                Debug.LogWarning("言語ファイルが存在しないため、生成します");
                File.Create(path + "/lang." + lanName).Close();
                continue;
            }

            //暗号化用変数
            byte[] ivBytes = null;
            byte[] base64Bytes = null;

            //ファイルを古典的な方法で開く(Resources.Loadはセーブデータ管理には不向きなので)
            using (FileStream file = new(path + "/lang." + lanName, FileMode.Open, FileAccess.Read))
            using (BinaryReader binary = new(file))
            {
                //4バイトずつ読み込む
                int length = binary.ReadInt32();
                ivBytes = binary.ReadBytes(length);

                length = binary.ReadInt32();
                base64Bytes = binary.ReadBytes(length);
            }

            string iv = Encoding.UTF8.GetString(ivBytes);
            string base64 = Encoding.UTF8.GetString(base64Bytes);
            //Base64で復号化
            DecryptAesBase64(base64, iv, out string json);

            // Jsonから言語データを生成
            _languageDataList.Add(language, JsonMapper.ToObject<LanguageData>(json));

        }


    }

    /// <summary>
    /// 言語ファイルを作成します
    /// </summary>
    /// <param name="language">言語の種類</param>
    /// <param name="data">言語データ</param>
    public static void SaveLanguageData(Language language, LanguageData data)
    {

        // Unknownはスキップ
        if (language.Equals(Language.Unknown))
        {
            Debug.LogWarning("Unknownの言語データは生成できません");
            return;
        }

        // Enumの文字列を取得する
        string lanName = Enum.GetName(typeof(Language), language);

        //ファイルパスを取得
#if UNITY_EDITOR
        string path = Directory.GetCurrentDirectory() + "\\Assets\\Resources";
#else
            string path = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
#endif
        if (!configData.dataFilePath.Equals("default"))
        {
            path = configData.dataFilePath;
        }

        //ファイルがあるかチェック
        if (!File.Exists(path + "/lang." + lanName))
        {
            Debug.LogWarning("言語ファイルが存在しないため、生成します");
            File.Create(path + "/lang." + lanName).Close();
        }

        string json = JsonMapper.ToJson(data);

        //Base64で暗号化
        EncryptAesBase64(json, out string iv, out string base64);

        // 保存
        byte[] ivBytes = Encoding.UTF8.GetBytes(iv);
        byte[] base64Bytes = Encoding.UTF8.GetBytes(base64);
        using FileStream file = new(path + "/lang." + lanName, FileMode.Create, FileAccess.Write);
        using BinaryWriter binary = new(file);
        //4バイトずつ書き込み
        binary.Write(ivBytes.Length);
        binary.Write(ivBytes);

        binary.Write(base64Bytes.Length);
        binary.Write(base64Bytes);
    }

    /// <summary>
    /// 現在の設定で扱っている言語ファイルを取得する
    /// </summary>
    public static LanguageData LanguageData
    {
        get
        {
            if (_languageDataList == null)
            {
                Debug.LogError("言語ファイルがロードされる前に、言語データが取得されました");
                return null;
            }
            if (configData.language == Language.Unknown)
            {
                Debug.LogError("設定ファイルが初期化されているため、言語を日本語に設定します");
                configData.language = Language.JP;
            }
            return _languageDataList[configData.language];
        }
    }

    /*以下編集は非推奨*/

    /// <summary>
    /// AES暗号化(Base64形式)
    /// </summary>
    public static void EncryptAesBase64(string json, out string iv, out string base64)
    {
        byte[] src = Encoding.UTF8.GetBytes(json);
        EncryptAes(src, out iv, out byte[] dst);
        base64 = Convert.ToBase64String(dst);
    }

    /// <summary>
    /// AES複合化(Base64形式)
    /// </summary>
    public static void DecryptAesBase64(string base64, string iv, out string json)
    {
        byte[] src = Convert.FromBase64String(base64);
        DecryptAes(src, iv, out byte[] dst);
        json = Encoding.UTF8.GetString(dst).Trim('\0');
    }

    /// <summary>
    /// AES暗号化
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
    /// AES複合化
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
    /// パスワード生成
    /// </summary>
    /// <param name="count">文字列数</param>
    /// <returns>パスワード</returns>
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
