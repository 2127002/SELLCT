using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
class TimeLimit
{
    [SerializeField] float _valueInSeconds;

    public TimeLimit(float valueInSeconds)
    {
        if (valueInSeconds < 0) throw new ArgumentException("�������Ԃ͕��̐��ɂ͂Ȃ�܂���B�l���������Ă��������B", nameof(valueInSeconds));

        _valueInSeconds = valueInSeconds;
    }

    /// <summary>
    /// �������Ԃ𖈃t���[�����炷�BUpdate�ɔz�u��z��
    /// </summary>
    /// �{���̒l�I�u�W�F�N�g�I�ɂ́A�V���ɐ������Ԃ�s�ςɂ��ăC���X�^���X�𐶐����ׂ��ł��B
    /// �������A���t���[���C���X�^���X�𐶐����鏈���͓��쑬�x�I�ɔ��������̂ł��̂悤�ɂ��܂��B
    public void DecreaseTimeDeltaTime()
    {
        _valueInSeconds -= Time.deltaTime;

        if (_valueInSeconds < 0) _valueInSeconds = 0;
    }

    /// <summary>
    /// �������Ԃɓ��B�������𔻒肷��
    /// </summary>
    /// <returns>���B�����Ftrue </returns>
    public bool IsTimeLimitReached()
    {
        return _valueInSeconds <= 0;
    }
}
