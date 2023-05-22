using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����t�F�[�Y�ɂ�����\���Ɋւ���N���X�ł��B
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
