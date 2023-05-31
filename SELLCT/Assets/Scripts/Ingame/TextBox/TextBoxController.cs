using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TextBoxController : MonoBehaviour
{
    [SerializeField] TextBoxView _textBoxView = default!;
    CancellationTokenSource _cancellationTokenSource = default!;

    /// <summary>
    /// 指定のテキストを表示します。.Forget()をつけない場合は下記エラーのcatchをお願いします。
    /// </summary>
    /// <param name="speaker">表示したい喋る人</param>
    /// <param name="message">表示したいメッセージ</param>
    /// <returns></returns>
    /// <exception cref="OperationCanceledException">新たなテキストが代入された際にこのエラーを返します。</exception>
    public async UniTask UpdateText(string speaker, string message)
    {
        _textBoxView.UpdateText(speaker, message);
        _textBoxView.UpdateDisplay();

        var cancellationToken = this.GetCancellationTokenOnDestroy();

        //前回の非同期処理が実行中であればキャンセルする
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        
        try
        {
            await _textBoxView.DisplayTextOneByOne();
        }
        catch (OperationCanceledException)
        {
            throw new OperationCanceledException("新たなテキストが代入されました。以前のテキスト処理を終了してください。");
        }

        try
        {
            await UniTask.Delay(2000, false, PlayerLoopTiming.FixedUpdate, _cancellationTokenSource.Token);
        }
        catch (OperationCanceledException)
        {
            //キャンセルされた場合は処理を終了する
            throw new OperationCanceledException("新たなテキストが代入されました。以前のテキスト処理を終了してください。");
        }

        _cancellationTokenSource = null;
    }

    public void Enable()
    {
        _textBoxView.Enable();
    }

    public void Disable()
    {
        _textBoxView.Disable();
    }
}
