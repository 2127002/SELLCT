using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

/// <summary>
/// フェード時間の値オブジェクトです。
/// </summary>
[Serializable]
class FadeTime
{
    [SerializeField, Min(0)] float _duration;
    [SerializeField, Min(0)] float _waitTime;

    float _value = 0;

    /// <summary>
    /// フェード時間の値オブジェクト
    /// </summary>
    /// <param name="fadeTime">時間（単位：s）</param>
    public FadeTime(float fadeTime)
    {
        if (fadeTime < 0) throw new ArgumentException("フェード時間は負の数にはなりません。値を見直してください。", nameof(_duration));

        _duration = fadeTime;
    }

    /// <summary>
    /// 時間を進める。動作させたい部分の
    /// </summary>
    public void AdvanceProgress()
    {
        _value += Time.deltaTime;
        if (_value > _duration) _value = _duration;
    }

    /// <summary>
    /// 進行度[0-1]
    /// </summary>
    /// <returns>開始…0 終了…1</returns>
    public float Progress()
    {
        return Mathf.Clamp01(_value / _duration);
    }

    public float WaitTime => _waitTime;
}
