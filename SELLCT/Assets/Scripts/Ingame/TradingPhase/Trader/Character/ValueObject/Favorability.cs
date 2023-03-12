using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Favorability
{
    const float MAX_AMOUNT = 100;
    const float MIN_AMOUNT = 0;

    readonly float _amount;

    bool _isLocked = false;

    public Favorability(float amount)
    {
        if (amount < MIN_AMOUNT) throw new System.ArgumentOutOfRangeException();
        if (amount > MIN_AMOUNT) throw new System.ArgumentOutOfRangeException();

        _amount = amount;
    }

    /// <summary>
    /// �D���x���グ��
    /// </summary>
    /// <param name="amount">�ǉ��������l</param>
    /// <returns>�V���ȃC���X�^���X</returns>
    public Favorability Add(Favorability amount)
    {
        if (_isLocked) return this;

        float newAmount = _amount + amount._amount;

        if (newAmount > MAX_AMOUNT) newAmount = MAX_AMOUNT;

        return new(newAmount);
    }

    /// <summary>
    /// �D���x��������
    /// </summary>
    /// <param name="amount">�������������l</param>
    /// <returns>�V���ȃC���X�^���X</returns>
    public Favorability Subtract(Favorability amount)
    {
        if (_isLocked) return this;

        float newAmount = _amount - amount._amount;

        if (newAmount < MIN_AMOUNT) newAmount = MIN_AMOUNT;

        return new(newAmount);
    }

    /// <summary>
    /// �D���x�G�������g������ꂽ�ꍇ�Ɏ��s
    /// </summary>
    /// <returns>�V���ȃC���X�^���X</returns>
    public Favorability SellCompanionElement()
    {
        //���g�̃C���X�^���X�����邱�ƂōD���x��0�ɂ���
        var value = Subtract(this);

        //�D���x�����b�N����B
        _isLocked = true;

        return value;
    }
}
