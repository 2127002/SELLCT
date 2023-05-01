using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ConversationController : MonoBehaviour
{
    [SerializeField] TraderController _traderController = default!;
    [SerializeField] TextBoxController _textBoxController = default!;

    const string DEFAULT = "default";

    private void Reset()
    {
        _traderController = FindFirstObjectByType<TraderController>();
        _textBoxController = FindFirstObjectByType<TextBoxController>();
    }

    public async UniTask OnStart()
    {
        string speaker = _traderController.CurrentTrader.Name;
        string[] startMessage = _traderController.CurrentTrader.StartMessage();

        //出会いのテキストを表示する
        for (int i = 0; i < startMessage.Length; i++)
        {
            try
            {
                await _textBoxController.UpdateText(speaker, startMessage[i].ToInBracketsText());
            }
            catch (OperationCanceledException)
            {
                //キャンセルされた場合は処理を終了する
                return;
            }
        }
    }

    public async UniTask OnEnd()
    {
        string speaker = _traderController.CurrentTrader.Name;
        string[] endMessage = _traderController.CurrentTrader.EndMessage();

        //テキストの表示
        for (int i = 0; i < endMessage.Length; i++)
        {
            try
            {
                await _textBoxController.UpdateText(speaker, endMessage[i].ToInBracketsText());
            }
            catch (OperationCanceledException)
            {
                //キャンセルされた場合は処理を終了する
                return;
            }
        }
    }

    public async UniTask OnSelect(Card card)
    {
        if (card.Id < 0) return;

        string speaker = _traderController.CurrentTrader.Name;
        string[] cardMessage = _traderController.CurrentTrader.CardMessage(card);

        for (int i = 0; i < cardMessage.Length; i++)
        {
            try
            {
                await _textBoxController.UpdateText(speaker, cardMessage[i].ToInBracketsText());
            }
            catch (OperationCanceledException)
            {
                //キャンセルされた場合は処理を終了する
                return;
            }
        }
    }

    public async UniTask OnBuy(Card card)
    {
        if (card.Id < 0) return;

        string speaker = _traderController.CurrentTrader.Name;
        string[] message = _traderController.CurrentTrader.BuyMessage(card);

        for (int i = 0; i < message.Length; i++)
        {
            try
            {
                await _textBoxController.UpdateText(speaker, message[i].ToInBracketsText());
            }
            catch (OperationCanceledException) { return; }
        }
    }

    public async UniTask OnSell(Card card)
    {
        if (card.Id < 0) return;

        string speaker = _traderController.CurrentTrader.Name;
        string[] message = _traderController.CurrentTrader.SellMessage(card);

        for (int i = 0; i < message.Length; i++)
        {
            try
            {
                await _textBoxController.UpdateText(speaker, message[i].ToInBracketsText());
            }
            catch (OperationCanceledException) { return; }
        }
    }
}
