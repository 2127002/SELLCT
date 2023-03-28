using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxView : MonoBehaviour
{
    [SerializeField, Min(0)] int _delayFrame = 3;

    //U5
    [SerializeField] Image _messageImage = default!;
    [SerializeField] TextMeshProUGUI _messageText = default!;

    //U13
    [SerializeField] Image _speakerImage = default!;
    [SerializeField] TextMeshProUGUI _speakerText = default!;

    CancellationTokenSource _cancellationTokenSource = default!;

    bool _imageEnabled;

    public async UniTask DisplayTextOneByOne()
    {
        var cancellationToken = this.GetCancellationTokenOnDestroy();

        //�O��̔񓯊����������s���ł���΃L�����Z������
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        string s = _messageText.text;

        for (int i = 0; i <= s.Length; i++)
        {
            // �͈͉��Z�q��p���Ă��܂��BSubstring�Ɠ������ʂł�
            string newText = s[..i];
            _messageText.text = newText;

            try
            {
                await UniTask.DelayFrame(_delayFrame, PlayerLoopTiming.FixedUpdate, _cancellationTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                //�L�����Z�����ꂽ�ꍇ�͏������I������
                return;
            }
        }

        // CancellationTokenSource��null�ɂ���
        _cancellationTokenSource = null;
    }

    /// <summary>
    /// �e�L�X�g��ύX���܂��B
    /// </summary>
    /// <param name="speaker"></param>
    /// <param name="speakerSprite"></param>
    /// <param name="message"></param>
    public void UpdateText(string speaker, string message)
    {
        _speakerText.text = speaker.ToDisplayString();
        _messageText.text = message.ToDisplayString();
    }

    public void UpdateDisplay()
    {
        bool spekerImageEnabled = !string.IsNullOrEmpty(_speakerText.text) && _imageEnabled;

        //Image������Text��E14�ɂ���č��E����Ȃ�
        _speakerImage.enabled = spekerImageEnabled;
        _messageImage.enabled = _imageEnabled;
    }

    public void Enable()
    {
        _messageImage.enabled = true;
        _speakerImage.enabled = true;
        _imageEnabled = true;
    }    
    
    public void Disable()
    {
        _messageImage.enabled = false;
        _speakerImage.enabled = false;
        _imageEnabled = false;
    }
}