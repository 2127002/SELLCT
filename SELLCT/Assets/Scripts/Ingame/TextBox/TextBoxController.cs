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
    /// �w��̃e�L�X�g��\�����܂��B.Forget()�����Ȃ��ꍇ�͉��L�G���[��catch�����肢���܂��B
    /// </summary>
    /// <param name="speaker">�\������������l</param>
    /// <param name="message">�\�����������b�Z�[�W</param>
    /// <returns></returns>
    /// <exception cref="OperationCanceledException">�V���ȃe�L�X�g��������ꂽ�ۂɂ��̃G���[��Ԃ��܂��B</exception>
    public async UniTask UpdateText(string speaker, string message)
    {
        _textBoxView.UpdateText(speaker, message);
        _textBoxView.UpdateDisplay();

        var cancellationToken = this.GetCancellationTokenOnDestroy();

        //�O��̔񓯊����������s���ł���΃L�����Z������
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        
        try
        {
            await _textBoxView.DisplayTextOneByOne();
        }
        catch (OperationCanceledException)
        {
            throw new OperationCanceledException("�V���ȃe�L�X�g���������܂����B�ȑO�̃e�L�X�g�������I�����Ă��������B");
        }

        try
        {
            await UniTask.Delay(2000, false, PlayerLoopTiming.FixedUpdate, _cancellationTokenSource.Token);
        }
        catch (OperationCanceledException)
        {
            //�L�����Z�����ꂽ�ꍇ�͏������I������
            throw new OperationCanceledException("�V���ȃe�L�X�g���������܂����B�ȑO�̃e�L�X�g�������I�����Ă��������B");
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
