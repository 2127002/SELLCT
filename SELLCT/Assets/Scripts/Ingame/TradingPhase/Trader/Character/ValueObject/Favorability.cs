using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Favorability
{
    public enum Classification
    {
        Class1,
        Class2,
        Class3,
        Class4,
        Class5,
        Class6,

        Invalid
    }

    FavourableView _favourableView;

    const float MAX_AMOUNT = 100;
    const float MIN_AMOUNT = 0;

    readonly float _amount;

    bool _isLocked = false;

    public Favorability(float amount, FavourableView favourableView)
    {
        if (amount < MIN_AMOUNT) throw new System.ArgumentOutOfRangeException();
        if (amount > MAX_AMOUNT) throw new System.ArgumentOutOfRangeException();
        if (favourableView == null) throw new System.ArgumentNullException();

        _amount = amount;
        _favourableView = favourableView;
        
        //�D���x�ϓ����ɍD���x�\���𒲐�
        _favourableView.Set(FavorabilityClassification(_amount));
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

        return new(newAmount, _favourableView);
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

        return new(newAmount, _favourableView);
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

    private Classification FavorabilityClassification(float amount)
    {
        if (amount == 0) return Classification.Class1;
        if (amount <= 24f) return Classification.Class2;
        if (amount <= 49f) return Classification.Class3;
        if (amount <= 74f) return Classification.Class4;
        if (amount <= 99f) return Classification.Class5;
        if (amount == 100f) return Classification.Class6;

        return Classification.Invalid;
    }
}
