using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndingView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text = default!;
    [SerializeField] FadeInView _fadeInView = default!;
    [SerializeField] TextFadeInView _textFadeinView = default!;
    [SerializeField] TextFadeOutView _textFadeoutView = default!;
    [SerializeField] Canvas _canvas = default!;

    private void Reset()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _fadeInView = GetComponent<FadeInView>();
        _textFadeinView = GetComponent<TextFadeInView>();
        _textFadeoutView = GetComponent<TextFadeOutView>();
        _canvas = GetComponentInChildren<Canvas>();
    }

    private void Awake()
    {
        _canvas.enabled = false;
    }

    public async UniTask StartEndingScene()
    {
        _canvas.enabled = true;

        _text.alpha = 0f;

        //���w�i�t�F�[�h�C��
        await _fadeInView.StartFade();

        //�e�L�X�g�t�F�[�h�C��
        await _textFadeinView.StartFade();

        //SE���Đ�

        //�e�L�X�g�t�F�[�h�A�E�g
        await _textFadeoutView.StartFade();
    }

    public void SetText(string text)
    {
        _text.text = text.ToDisplayString();
    }
}
