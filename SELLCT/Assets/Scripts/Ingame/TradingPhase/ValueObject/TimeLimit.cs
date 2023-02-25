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
        if (valueInSeconds < 0) throw new ArgumentException("制限時間は負の数にはなりません。値を見直してください。", nameof(valueInSeconds));

        _valueInSeconds = valueInSeconds;
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
