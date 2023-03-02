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

        //前回の非同期処理が実行中であればキャンセルする
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        string s = _text.text;

        for (int i = 0; i <= s.Length; i++)
        {
            // 範囲演算子を用いています。Substringと同じ効果です
            string newText = s[..i];
            _text.text = newText;

            try
            {
                await UniTask.DelayFrame(_delayFrame, PlayerLoopTiming.FixedUpdate, _cancellationTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                //キャンセルされた場合は処理を終了する
                return;
            }
        }

        // CancellationTokenSourceをnullにする
        _cancellationTokenSource = null;
    }
    public void UpdeteText(string text)
    {
        _text.text = text;
    }
}