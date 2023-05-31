using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Money
{
    readonly int _amount;
    public static readonly Money Zero = new(0);

    public Money(int amount)
    {
        //���̐��̃`�F�b�N�����Ă��܂��񂪁A�؋��Ƃ����`�ŕ��̐��ɂȂ�ꍇ�����邽�߂ł��B
        _amount = amount;
    }

    /// <summary>
    /// �������𑝂₵�܂��B
    /// </summary>
    /// <param name="value"></param>
    /// <returns>�v�Z���Money�C���X�^���X</returns>
    public Money Add(Money value)
    {
        //�ő�l�͐ݒ肵�Ă��܂���B

        return new(_amount + value._amount);
    }

    /// <summary>
    /// ���������w��l�����炵�܂�
    /// </summary>
    /// <param name="value"></param>
    /// <returns>�v�Z���Money�C���X�^���X</returns>
    /// <exception cref="NegativeMoneyException">�؋���Ԏ��ɍX�Ɏ؋����悤�Ƃ���ƋN����</exception>
    public Money Subtract(Money value)
    {
        if (_amount <= Zero._amount) throw new NegativeMoneyException();

        return new(_amount - value._amount);
    }

    public int CurrentAmount()
    {
        return _amount;
    }

    public static bool operator <(Money l, Money r)
    {
        return l._amount < r._amount;
    }    
    public static bool operator >(Money l, Money r)
    {
        return l._amount > r._amount;
    }    
    public static bool operator <=(Money l, Money r)
    {
        return l._amount <= r._amount;
    }    
    public static bool operator >=(Money l, Money r)
    {
        return l._amount >= r._amount;
    }
}

/// <summary>
/// �؋���Ԏ��ɍX�Ɏ؋����悤�Ƃ���ƋN����
/// </summary>
public class NegativeMoneyException : Exception
{
    //�R���X�g���N�^�ȊO�A�����ɏ����͏����Ȃ��ł��������B
    public NegativeMoneyException()
    {
    }
}