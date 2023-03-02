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
    [SerializeField] FadeInView _fadeinView;
    [SerializeField] FadeOutView _fadeoutView;

    private void Start()
    {
        _fadeinView.StartFade().Forget();
    }

    public async UniTask StartFadeout()
    {
       await _fadeoutView.StartFade();
    }
}
