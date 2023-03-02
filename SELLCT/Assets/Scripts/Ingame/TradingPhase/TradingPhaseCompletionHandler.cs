using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
