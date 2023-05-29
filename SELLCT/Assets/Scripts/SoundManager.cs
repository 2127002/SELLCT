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
    BGM1_NONAME,

    //SE
    SE1_,//この項目はSEの先頭固定でお願いします。

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

        EditorGUILayout.HelpBox("BGM、SEを追加する際は順番に注意してください。", MessageType.Info);

        EditorGUILayout.Separator();

#pragma warning disable CS0618 // 型またはメンバーが旧型式です
        EditorGUILayout.LabelField(soundManager._debugSoundSource >= SoundSource.SE1_ ? "SE Clip : Element " + (soundManager._debugSoundSource - SoundSource.SE1_) : "BGM Clip : Element " + (int)soundManager._debugSoundSource);
#pragma warning restore CS0618 // 型またはメンバーが旧型式です
        EditorGUILayout.Separator();

        base.OnInspectorGUI();

    }
}
#endif

public class SoundManager : MonoBehaviour
{
    [Tooltip("BGM、SEの追加場所を取得できます。")]
    [Obsolete("デバッグ用に用意された変数です。これを使用することはできません。", false)]
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

    private void Awake()
    {
        DontDestroyOnLoad(this);

        Instance = this;

        CreateSEs();
    }

    private void Start()
    {
        InitAudioMixerGroup();
    }

    /// <summary>
    /// ボリュームからデシベルへの変換
    /// </summary>
    /// <param name="volume">変換させたいボリューム（0-1）</param>
    /// <returns>デシベル（-80 ~ 0）</returns>
    public static float ConvertVolume2dB(float volume) => Mathf.Clamp(20f * Mathf.Log10(Mathf.Clamp(volume, 0f, 1f)), -80f, 0f);

    /// <summary>
    /// デシベルからボリュームへの変換
    /// </summary>
    /// <param name="db">変換させたいデシベル（-80 ~ 0）</param>
    /// <returns>ボリューム（0-1）</returns>
    public static float ConvertDB2Volume(float db) => Mathf.Clamp(Mathf.Pow(10, Mathf.Clamp(db, -80, 0) / 20f), 0, 1);

    private void InitAudioMixerGroup()
    {
        _audioMixer.SetFloat("MasterVolume", ConvertVolume2dB(DataManager.configData.masterVolume / 10f));
        _audioMixer.SetFloat("BGMVolume", ConvertVolume2dB(DataManager.configData.backGroundMusicVolume / 10f));
        _audioMixer.SetFloat("SEVolume", ConvertVolume2dB(DataManager.configData.soundEffectVolume / 10f));
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
    /// BGMをかけます。
    /// </summary>
    /// <param name="sound">かけたいBGM</param>
    public void PlayBGM(SoundSource sound, float time = 0)
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

    public void PlayBGM(SoundSource sound, float fadeTime, float time = 0, float volume = DEFAULT_BGM_VOLUME)
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
    /// SEを再生します。10チャンネルすべて利用されていた場合流れません
    /// </summary>
    /// <param name="sound">再生したいSE</param>
    /// <param name="stopPrebSE">以前に再生している同じSEを停止させるか否か</param>    
    public void PlaySE(int sound, bool stopPrebSE = false, float fadeTime = 0, float endVolume = DEFAULT_SE_VOLUME)
    {
        if (stopPrebSE) StopSE(sound);

        foreach (var se in SEs)
        {
            if (se.isPlaying) continue;

            ChangeSE(se, (SoundSource)sound);
            SEFadein(se, fadeTime, endVolume).Forget();
            se.Play();
            return;
        }
        //すべてのチャンネルが使用中ならここにくる
        Debug.LogWarning("全SEチャンネルが使用中で" + sound + "が再生できませんでした");
    }

    /// <summary>
    /// SEを再生します。10チャンネルすべて利用されていた場合流れません
    /// </summary>
    /// <param name="sound">再生したいSE</param>
    /// <param name="stopPrebSE">以前に再生している同じSEを停止させるか否か</param>    
    public void PlaySE(SoundSource sound, bool stopPrebSE = false, float fadeTime = 0, float endVolume = DEFAULT_SE_VOLUME)
    {
        PlaySE((int)sound, stopPrebSE, fadeTime, endVolume);
    }

    /// <summary>
    /// すべてのSEを停止させます。
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
    /// 指定のSEをすべて停止させます
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
    /// オーディオミキサーの値を変更します
    /// </summary>
    /// <param name="name"></param>
    /// <param name="dB"></param>
    public void ChangeAudioMixerDB(string name, float dB)
    {
        _audioMixer.SetFloat(name, dB);
    }
}
