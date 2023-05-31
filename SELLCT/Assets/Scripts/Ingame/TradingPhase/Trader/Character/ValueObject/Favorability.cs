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

        //好感度変動時に好感度表示を調整
        _favourableView.Set(FavorabilityClassification());
    }

    /// <summary>
    /// 好感度を上げる
    /// </summary>
    /// <param name="amount">追加したい値</param>
    /// <returns>新たなインスタンス</returns>
    public Favorability Add(Favorability amount)
    {
        if (_isLocked) return this;

        float newAmount = _amount + amount._amount;

        newAmount = Mathf.Min(newAmount, MAX_AMOUNT);

        return new(newAmount, _favourableView);
    }

    /// <summary>
    /// 好感度を下げる
    /// </summary>
    /// <param name="amount">減少させたい値</param>
    /// <returns>新たなインスタンス</returns>
    public Favorability Subtract(Favorability amount)
    {
        if (_isLocked) return this;

        float newAmount = _amount - amount._amount;

        newAmount = Mathf.Max(newAmount, MIN_AMOUNT);

        return new(newAmount, _favourableView);
    }

    /// <summary>
    /// 好感度エレメントが売られた場合に実行
    /// </summary>
    /// <returns>新たなインスタンス</returns>
    public Favorability SellCompanionElement()
    {
        //自身のインスタンスを入れることで好感度を0にする
        var value = Subtract(this);

        //好感度をロックする。
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
