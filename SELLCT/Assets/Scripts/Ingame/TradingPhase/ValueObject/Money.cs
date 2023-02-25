using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Money
{
    readonly int _amount;

    public Money(int amount)
    {
        //負の数のチェックをしていませんが、借金という形で負の数になる場合があるためです。
        _amount = amount;
    }

    /// <summary>
    /// 所持金を増やします。
    /// </summary>
    /// <param name="value"></param>
    /// <returns>計算後のMoneyインスタンス</returns>
    public Money Add(Money value)
    {
        //最大値は設定していません。

        return new(_amount + value._amount);
    }

    /// <summary>
    /// 所持金を指定値分減らします
    /// </summary>
    /// <param name="value"></param>
    /// <returns>計算後のMoneyインスタンス</returns>
    /// <exception cref="NegativeMoneyException">借金状態時に更に借金しようとすると起こる</exception>
    public Money Subtract(Money value)
    {
        if (_amount <= 0) throw new NegativeMoneyException();

        return new(_amount - value._amount);
    }
}

/// <summary>
/// 借金状態時に更に借金しようとすると起こる
/// </summary>
public class NegativeMoneyException : Exception
{
    //コンストラクタ以外、内部に処理は書かないでください。
    public NegativeMoneyException()
    {
        Debug.LogError("try-catch節を用いて適切に処理してください");
    }
}