using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class TextBoxView : MonoBehaviour
{
    [SerializeField, Min(0)] int _delayFrame = 3;
    [SerializeField] TextMeshProUGUI _text;

    CancellationTokenSource _cancellationTokenSource;

    public async UniTask DisplayTextOneByOne()
    {
        var cancellationToken = this.GetCancellationTokenOnDestroy();

        //�O��̔񓯊����������s���ł���΃L�����Z������
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        string s = _text.text;

        for (int i = 0; i <= s.Length; i++)
        {
            // �͈͉��Z�q��p���Ă��܂��BSubstring�Ɠ������ʂł�
            string newText = s[..i];
            _text.text = newText;

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
    public void UpdeteText(string text)
    {
        _text.text = text;
    }
}