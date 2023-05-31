using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

/// <summary>
/// �t�F�[�h���Ԃ̒l�I�u�W�F�N�g�ł��B
/// </summary>
[Serializable]
class FadeTime
{
    [SerializeField, Min(0)] float _duration;
    [SerializeField, Min(0)] float _waitTime;

    float _value = 0;

    /// <summary>
    /// �t�F�[�h���Ԃ̒l�I�u�W�F�N�g
    /// </summary>
    /// <param name="fadeTime">���ԁi�P�ʁFs�j</param>
    public FadeTime(float fadeTime)
    {
        if (fadeTime < 0) throw new ArgumentException("�t�F�[�h���Ԃ͕��̐��ɂ͂Ȃ�܂���B�l���������Ă��������B", nameof(_duration));

        _duration = fadeTime;
    }

    /// <summary>
    /// ���Ԃ�i�߂�B���삳������������
    /// </summary>
    public void AdvanceProgress()
    {
        _value += Time.deltaTime;
        if (_value > _duration) _value = _duration;
    }

    /// <summary>
    /// �i�s�x[0-1]
    /// </summary>
    /// <returns>�J�n�c0 �I���c1</returns>
    public float Progress()
    {
        return Mathf.Clamp01(_value / _duration);
    }

    public float WaitTime => _waitTime;
}
