using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 売買フェーズにおける表示に関するクラスです。
/// </summary>
[RequireComponent(typeof(FadeInView),typeof(FadeOutView))]
public class TradingPhaseView : MonoBehaviour
{
    [SerializeField] FadeInView _fadeinView = default!;
    [SerializeField] FadeOutView _fadeoutView = default!;

    public void OnPhaseStart()
    {
        _fadeinView.StartFade().Forget();
    }

    public async UniTask OnPhaseComplete()
    {
        await _fadeoutView.StartFade();
    }
}
