using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.Audio;
#if UNITY_EDITOR
using UnityEditor;
#endif

public enum SoundSource
{
    //BGM
    BGM01_NORMAL,
    BGM02_TITLE,
    BGM03_ENDING,
    BGM04_,
    BGM05_,
    BGM06_,
    BGM07_,
    BGM08_,
    BGM09_,

    //SE
    SE01_DECIDE,//���̍��ڂ�SE�̐擪�Œ�ł��肢���܂��B
    SE02_CURSOR,
    SE03_CROSSHAIR,
    SE04_RUG_START,
    SE05_RUG_END,
    SE06_ITEM_START,
    SE07_ITEM_END,
    SE08_HUNDSTART,
    SE09_TIME,
    SE10_TIMELIMIT,
    SE11_BUY,
    SE12_SELL,
    SE13_TEXT,
    SE14_SCENE_CHANGE,
    SE15_UI_START,
    SE16_HERAT,
    SE17_CLOCK,
    SE18_NOISE,
    SE19_NECKBREAK,
    SE20_DOOR_OPEN,
    SE21_DOOR_CLOSE,
    SE22_WALK,
    SE23_KNIFE_STAB,
    SE24_BLOOD_DRIP,
    SE25_CARD_SELECT,
}

#if UNITY_EDITOR
[CustomEditor(typeof(SoundManager))]
public class SoundManagerOnGUI : Editor
{
    private SoundManager soundManager;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        soundManager = target as SoundManager;

        EditorGUILayout.HelpBox("BGM�ASE��ǉ�����ۂ͏��Ԃɒ��ӂ��Ă��������B", MessageType.Info);

        EditorGUILayout.Separator();

#pragma warning disable CS0618 // �^�܂��̓����o�[�����^���ł�
        EditorGUILayout.LabelField(soundManager._debugSoundSource >= SoundSource.SE01_DECIDE ? "SE Clip : Element " + (soundManager._debugSoundSource - SoundSource.SE01_DECIDE) : "BGM Clip : Element " + (int)soundManager._debugSoundSource);
#pragma warning restore CS0618 // �^�܂��̓����o�[�����^���ł�
        EditorGUILayout.Separator();

        base.OnInspectorGUI();

    }
}
#endif

public enum MixerGroup
{
    Master,
    BGM,
    SE,
}

public class SoundManager : MonoBehaviour
{
    [Tooltip("BGM�ASE�̒ǉ��ꏊ���擾�ł��܂��B")]
    [Obsolete("�f�o�b�O�p�ɗp�ӂ��ꂽ�ϐ��ł��B������g�p���邱�Ƃ͂ł��܂���B", false)]
    public SoundSource _debugSoundSource;

    public static SoundManager Instance { get; private set; }

    private readonly AudioSource[] SEs = new AudioSource[SENum];
    private const int SENum = 10;

    [SerializeField] private AudioSource _BGMLoop;
    [SerializeField] private AudioSource _BGMIntro;
    [SerializeField] private AudioSource _SEPrefab;

    [SerializeField] private AudioClip[] BGMClip;
    [SerializeField] private AudioClip[] SEClip;

    [SerializeField] private AudioMixer _audioMixer;
    const float DEFAULT_BGM_VOLUME = 1f;
    const float DEFAULT_SE_VOLUME = 1f;

    public float BGMLoopTime => _BGMLoop.time;


    readonly Dictionary<MixerGroup, string> mixerStr = new(){
        {MixerGroup.Master, "MasterVolume"},
        {MixerGroup.BGM, "BGMVolume"},
        {MixerGroup.SE, "SEVolume"},
    };

    Dictionary<MixerGroup, int> mixerData;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        Instance = this;

        CreateSEs();
    }

    private void Start()
    {
        InitAudioMixerGroup();

        mixerData = new(){
        {MixerGroup.Master, DataManager.configData.masterVolume},
        {MixerGroup.BGM, DataManager.configData.backGroundMusicVolume},
        {MixerGroup.SE, DataManager.configData.soundEffectVolume},
         };
    }

    /// <summary>
    /// �{�����[������f�V�x���ւ̕ϊ�
    /// </summary>
    /// <param name="volume">�ϊ����������{�����[���i0-1�j</param>
    /// <returns>�f�V�x���i-80 ~ 0�j</returns>
    public static float ConvertVolume2dB(float volume) => Mathf.Clamp(20f * Mathf.Log10(Mathf.Clamp(volume, 0f, 1f)), -80f, 0f);

    /// <summary>
    /// �f�V�x������{�����[���ւ̕ϊ�
    /// </summary>
    /// <param name="db">�ϊ����������f�V�x���i-80 ~ 0�j</param>
    /// <returns>�{�����[���i0-1�j</returns>
    public static float ConvertDB2Volume(float db) => Mathf.Clamp(Mathf.Pow(10, Mathf.Clamp(db, -80, 0) / 20f), 0, 1);

    private void InitAudioMixerGroup()
    {
        _audioMixer.SetFloat("MasterVolume", ConvertVolume2dB(DataManager.configData.masterVolume / 10f));
        _audioMixer.SetFloat("BGMVolume", ConvertVolume2dB(DataManager.configData.backGroundMusicVolume / 10f));
        _audioMixer.SetFloat("SEVolume", ConvertVolume2dB(DataManager.configData.soundEffectVolume / 10f));
    }

    /// <summary>
    /// ���ʂ̒���������B
    /// </summary>
    /// <param name="mixerGroup">�����������~�L�T�[�O���[�v</param>
    /// <param name="volume">�{�����[��(0-1)</param>
    public void SetAudioMixerValue(MixerGroup mixerGroup, float volume)
    {
        mixerStr.TryGetValue(mixerGroup, out string value);
        mixerData.TryGetValue(mixerGroup, out int dataVolume);

        float saveDateVolume = dataVolume / 10f;

        _audioMixer.SetFloat(value, ConvertVolume2dB(saveDateVolume * volume));
    }

    private void CreateSEs()
    {
        for (int i = 0; i < SENum; i++)
        {
            SEs[i] = Instantiate(_SEPrefab);
            SEs[i].name = ("SE" + (i + 1));
            SEs[i].transform.parent = transform;
        }
    }

    private void ChangeBGM(AudioSource audioSource, SoundSource sound)
    {
        int temp = 0;
        foreach (var bgm in BGMClip)
        {
            if ((int)sound == temp)
            {
                audioSource.clip = bgm;
                temp = 0;
                break;
            }
            else temp++;
        }
    }

    /// <summary>
    /// BGM�������܂��B
    /// </summary>
    /// <param name="sound">��������BGM</param>
    public void PlayBGM(SoundSource sound, float time = 0f)
    {
        ChangeBGM(_BGMLoop, sound);
        _BGMLoop.time = time;
        _BGMLoop.Play();
    }

    public void PlayBGM(SoundSource intro, SoundSource loop)
    {
        ChangeBGM(_BGMIntro, intro);
        _BGMIntro.PlayScheduled(AudioSettings.dspTime);
        ChangeBGM(_BGMLoop, loop);
        _BGMLoop.PlayScheduled(AudioSettings.dspTime + (_BGMIntro.clip.samples / (float)_BGMIntro.clip.frequency));
    }

    public void PlayBGM(SoundSource sound, float fadeTime, float time = 0f, float volume = DEFAULT_BGM_VOLUME)
    {
        ChangeBGM(_BGMLoop, sound);
        _BGMLoop.volume = 0;
        _BGMLoop.time = time;
        _BGMLoop.Play();
        BGMFadein(fadeTime, volume).Forget();
    }

    public void StopBGM()
    {
        _BGMIntro.Stop();
        _BGMLoop.Stop();
    }

    public async void StopBGM(float fadeTime)
    {
        await BGMFadeout(fadeTime);

        StopBGM();
        _BGMLoop.volume = DEFAULT_BGM_VOLUME;
    }

    private async UniTask BGMFadein(float time, float volume)
    {
        if (time <= 0) return;

        float fadeTime = 0;
        float lastVolume = volume;

        while (_BGMLoop.volume < lastVolume)
        {
            await UniTask.Yield();
            fadeTime += Time.deltaTime;

            _BGMLoop.volume = Mathf.Min(lastVolume * (fadeTime / time), lastVolume);
        }
    }
    private async UniTask BGMFadeout(float time)
    {
        if (time <= 0) return;

        float fadeTime = 0;
        float firstVolume = _BGMLoop.volume;

        while (_BGMLoop.volume > 0)
        {
            await UniTask.Yield();
            fadeTime += Time.deltaTime;

            _BGMLoop.volume = Mathf.Max(firstVolume * (1 - (fadeTime / time)), 0);
        }
    }

    private void ChangeSE(AudioSource _as, SoundSource sound)
    {
        int temp = BGMClip.Length;
        foreach (var se in SEClip)
        {
            if ((int)sound == temp)
            {
                _as.clip = se;
                temp = BGMClip.Length;
                break;
            }
            else temp++;
        }
    }

    /// <summary>
    /// SE���Đ����܂��B10�`�����l�����ׂė��p����Ă����ꍇ����܂���
    /// </summary>
    /// <param name="sound">�Đ�������SE</param>
    /// <param name="stopPrebSE">�ȑO�ɍĐ����Ă��铯��SE���~�����邩�ۂ�</param>    
    public void PlaySE(int sound, float volume = 1f, bool stopPrebSE = false, float fadeTime = 0, float endVolume = DEFAULT_SE_VOLUME)
    {
        if (stopPrebSE) StopSE(sound);

        foreach (var se in SEs)
        {
            if (se.isPlaying) continue;

            ChangeSE(se, (SoundSource)sound);
            SEFadein(se, fadeTime, endVolume).Forget();
            se.volume = volume;
            if ((int)SoundSource.SE13_TEXT == sound)
            {
                se.pitch = UnityEngine.Random.Range(1f - 0.2f, 1f + 0.1f);
            }
            else
            {
                se.pitch = 1f;
            }
            se.Play();
            return;
        }
        //���ׂẴ`�����l�����g�p���Ȃ炱���ɂ���
        Debug.LogWarning("�SSE�`�����l�����g�p����" + (SoundSource)sound + "���Đ��ł��܂���ł���");
    }

    /// <summary>
    /// SE���Đ����܂��B10�`�����l�����ׂė��p����Ă����ꍇ����܂���
    /// </summary>
    /// <param name="sound">�Đ�������SE</param>
    /// <param name="stopPrebSE">�ȑO�ɍĐ����Ă��铯��SE���~�����邩�ۂ�</param>    
    public void PlaySE(SoundSource sound, float volume = 1f, bool stopPrebSE = false, float fadeTime = 0, float endVolume = DEFAULT_SE_VOLUME)
    {
        PlaySE((int)sound, volume, stopPrebSE, fadeTime, endVolume);
    }

    /// <summary>
    /// ���ׂĂ�SE���~�����܂��B
    /// </summary>
    public void StopAllSE()
    {
        foreach (AudioSource se in SEs)
        {
            if (!se.isPlaying) continue;

            se.Stop();
        }
    }

    /// <summary>
    /// �w���SE�����ׂĒ�~�����܂�
    /// </summary>
    /// <param name="sound"></param>
    public void StopSE(SoundSource sound, float fadeTime = 0)
    {
        StopSE((int)sound, fadeTime);
    }

    public async void StopSE(int sound, float fadeTime = 0)
    {
        foreach (AudioSource se in SEs)
        {
            if (!se.isPlaying) continue;
            if (se.clip == null) continue;
            if (SEClip[sound - BGMClip.Length] != se.clip) continue;

            await SEFadeout(se, fadeTime);
            se.Stop();
        }
    }

    private async UniTask SEFadein(AudioSource source, float time, float volume)
    {
        if (time <= 0)
        {
            source.volume = volume;
            return;
        }
        float fadeTime = 0;
        float lastVolume = volume;

        while (source.volume < lastVolume)
        {
            await UniTask.Yield();
            fadeTime += Time.deltaTime;

            source.volume = Mathf.Min(lastVolume * (fadeTime / time), lastVolume);
        }
    }

    private async UniTask SEFadeout(AudioSource source, float time)
    {
        if (time <= 0) return;

        float fadeTime = 0;
        float firstVolume = _BGMLoop.volume;

        while (source.volume > 0)
        {
            await UniTask.Yield();
            fadeTime += Time.deltaTime;

            source.volume = Mathf.Max(firstVolume * (1 - (fadeTime / time)), 0);
        }
    }

    /// <summary>
    /// �I�[�f�B�I�~�L�T�[�̒l��ύX���܂�
    /// </summary>
    /// <param name="name"></param>
    /// <param name="dB"></param>
    public void ChangeAudioMixerDB(string name, float dB)
    {
        _audioMixer.SetFloat(name, dB);
    }
}
