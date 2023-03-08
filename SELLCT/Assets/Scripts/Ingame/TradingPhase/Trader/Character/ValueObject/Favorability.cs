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
    /// 好感度を上げる
    /// </summary>
    /// <param name="amount">追加したい値</param>
    /// <returns>新たなインスタンス</returns>
    public Favorability Add(Favorability amount)
    {
        if (_isLocked) return this;

        float newAmount = _amount + amount._amount;

        if (newAmount > MAX_AMOUNT) newAmount = MAX_AMOUNT;

        return new(newAmount);
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

        if (newAmount < MIN_AMOUNT) newAmount = MIN_AMOUNT;

        return new(newAmount);
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
}
