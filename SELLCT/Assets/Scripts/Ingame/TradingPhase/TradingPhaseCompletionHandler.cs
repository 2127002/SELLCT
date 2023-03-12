using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 売買フェーズの終了したことを知らせるクラス
/// </summary>
public class TradingPhaseCompletionHandler : MonoBehaviour
{
    Action _onComplete;

    public void AddListener(Action action)
    {
        _onComplete += action;
    }
    
    public void RemoveListener(Action action)
    {
        _onComplete -= action;
    }

    /// <summary>
    /// 売買フェーズ終了時処理
    /// </summary>
    public void OnComplete()
    {
        _onComplete?.Invoke();
    }
}
