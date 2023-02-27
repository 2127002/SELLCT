using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITradingPhaseViewReceiver
{
    void OnTimeLimitReached();
}

/// <summary>
/// �����t�F�[�Y�ɂ�����\���Ɋւ���N���X�ł��B
/// </summary>
[RequireComponent(typeof(FadeInView),typeof(FadeOutView))]
public class TradingPhaseView : MonoBehaviour, ITradingPhaseViewReceiver
{
    [SerializeField] FadeInView _fadeinView;
    [SerializeField] FadeOutView _fadeoutView;

    private void Start()
    {
        _fadeinView.StartFade().Forget();
    }

    public void OnTimeLimitReached()
    {
        _fadeoutView.StartFade().Forget();
    }
}
