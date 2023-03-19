using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLimit
{
    //��������
    float _valueInSeconds;

    //�ő吧�����Ԃ̌v�Z�Ɏg�p
    float _timeLimitRate;

    public TimeLimit(float valueInSeconds, float timeLimitRate)
    {
        if (valueInSeconds < 0) throw new ArgumentException("�������Ԃ͕��̐��ɂ͂Ȃ�܂���B�l���������Ă��������B", nameof(valueInSeconds));

        _valueInSeconds = valueInSeconds;
        _timeLimitRate = timeLimitRate;
    }

    /// <summary>
    /// �������Ԃ̒ǉ����s���B
    /// </summary>
    /// <param name="addValue">�ǉ�����������</param>
    /// <returns>�ǉ���̃C���X�^���X</returns>
    public TimeLimit AddTimeLimit(TimeLimit addValue, int currentE24Count)
    {
        float newTimeLimit = _valueInSeconds + addValue._valueInSeconds;

        //�ő吧�����Ԃ𒴉߂��Ă�����ő吧�����Ԃɂ���
        newTimeLimit = Mathf.Min(newTimeLimit, currentE24Count * _timeLimitRate);

        return new TimeLimit(newTimeLimit, _timeLimitRate);
    }

    /// <summary>
    /// �������Ԃ̌������s���B
    /// </summary>
    /// <param name="addValue">�ǉ�����������</param>
    /// <returns>������̃C���X�^���X</returns>
    public TimeLimit ReduceTimeLimit(TimeLimit reduceValue, int currentE24Count)
    {
        float newTimeLimit = MathF.Max(_valueInSeconds - reduceValue._valueInSeconds, 0);

        //�ő吧�����Ԃ𒴉߂��Ă�����ő吧�����Ԃɂ���
        newTimeLimit = Mathf.Min(newTimeLimit, currentE24Count * _timeLimitRate);

        return new TimeLimit(newTimeLimit, _timeLimitRate);
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
