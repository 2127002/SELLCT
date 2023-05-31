using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Favorability
{
    public enum Classification
    {
        Class1 = 1,
        Class2 = 2,
        Class3 = 3,
        Class4 = 4,

        Invalid
    }

    readonly FavourableView _favourableView;

    const float MAX_AMOUNT = 100f;
    const float MIN_AMOUNT = 0f;

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
        _favourableView.Set(FavorabilityClassification());
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

        newAmount = Mathf.Min(newAmount, MAX_AMOUNT);

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

        newAmount = Mathf.Max(newAmount, MIN_AMOUNT);

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

    public Classification FavorabilityClassification()
    {
        const float MARGIN = 0.000001f;
        if (_amount <= MARGIN) return Classification.Class1;
        else if (_amount <= 50f) return Classification.Class2;
        else if (_amount <= 99f) return Classification.Class3;
        return Classification.Class4;
    }
}
