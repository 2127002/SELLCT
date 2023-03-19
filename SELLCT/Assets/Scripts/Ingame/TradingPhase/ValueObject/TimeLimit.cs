using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLimit
{
    //制限時間
    float _valueInSeconds;

    //最大制限時間の計算に使用
    float _timeLimitRate;

    public TimeLimit(float valueInSeconds, float timeLimitRate)
    {
        if (valueInSeconds < 0) throw new ArgumentException("制限時間は負の数にはなりません。値を見直してください。", nameof(valueInSeconds));

        _valueInSeconds = valueInSeconds;
        _timeLimitRate = timeLimitRate;
    }

    /// <summary>
    /// 制限時間の追加を行う。
    /// </summary>
    /// <param name="addValue">追加したい時間</param>
    /// <returns>追加後のインスタンス</returns>
    public TimeLimit AddTimeLimit(TimeLimit addValue, int currentE24Count)
    {
        float newTimeLimit = _valueInSeconds + addValue._valueInSeconds;

        //最大制限時間を超過していたら最大制限時間にする
        newTimeLimit = Mathf.Min(newTimeLimit, currentE24Count * _timeLimitRate);

        return new TimeLimit(newTimeLimit, _timeLimitRate);
    }

    /// <summary>
    /// 制限時間の減少を行う。
    /// </summary>
    /// <param name="addValue">追加したい時間</param>
    /// <returns>減少後のインスタンス</returns>
    public TimeLimit ReduceTimeLimit(TimeLimit reduceValue, int currentE24Count)
    {
        float newTimeLimit = MathF.Max(_valueInSeconds - reduceValue._valueInSeconds, 0);

        //最大制限時間を超過していたら最大制限時間にする
        newTimeLimit = Mathf.Min(newTimeLimit, currentE24Count * _timeLimitRate);

        return new TimeLimit(newTimeLimit, _timeLimitRate);
    }

    /// <summary>
    /// 制限時間を毎フレーム減らす。Updateに配置を想定
    /// </summary>
    /// 本来の値オブジェクト的には、新たに制限時間を不変にしてインスタンスを生成すべきです。
    /// しかし、毎フレームインスタンスを生成する処理は動作速度的に避けたいのでこのようにします。
    public void DecreaseTimeDeltaTime()
    {
        _valueInSeconds -= Time.deltaTime;

        if (_valueInSeconds < 0) _valueInSeconds = 0;
    }

    /// <summary>
    /// 制限時間に到達したかを判定する
    /// </summary>
    /// <returns>到達した：true </returns>
    public bool IsTimeLimitReached()
    {
        return _valueInSeconds <= 0;
    }
}
