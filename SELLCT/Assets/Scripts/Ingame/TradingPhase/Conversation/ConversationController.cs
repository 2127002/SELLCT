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

    private void Reset()
    {
        _traderController = FindFirstObjectByType<TraderController>();
        _textBoxController = FindFirstObjectByType<TextBoxController>();
    }

    public async UniTask OnStart()
    {
        string speaker = _traderController.CurrentTrader.Name;
        var startMessage = _traderController.CurrentTrader.StartMessage();

        //�o��̃e�L�X�g��\������
        for (int i = 0; i < startMessage.message.Length; i++)
        {
            //1-indexed��0-indexed�ɕϊ�����
            Sprite sprite = _traderController.CurrentTrader.TraderSprites[startMessage.face[i] - 1];
            string message = startMessage.message[i].ToInBracketsText();
            _traderController.SetTraderSprite(sprite);
            try
            {
                await _textBoxController.UpdateText(speaker, message);
            }
            catch (OperationCanceledException)
            {
                //�L�����Z�����ꂽ�ꍇ�͕\����f�t�H���g�ɖ߂��ď������I������
                return;
            }
        }
    }

    public async UniTask OnEnd()
    {
        string speaker = _traderController.CurrentTrader.Name;
        var endMessage = _traderController.CurrentTrader.EndMessage();

        //�o��̃e�L�X�g��\������
        for (int i = 0; i < endMessage.message.Length; i++)
        {            
            //1-indexed��0-indexed�ɕϊ�����
            Sprite sprite = _traderController.CurrentTrader.TraderSprites[endMessage.face[i] - 1];
            string message = endMessage.message[i].ToInBracketsText();

            _traderController.SetTraderSprite(sprite);
            try
            {
                await _textBoxController.UpdateText(speaker, message);
            }
            catch (OperationCanceledException)
            {
                return;
            }
        }
    }

    public async UniTask OnSelect(Card card)
    {
        if (card.Id < 0) return;

        string speaker = _traderController.CurrentTrader.Name;
        var cardMessage = _traderController.CurrentTrader.CardMessage(card);

        //�o��̃e�L�X�g��\������
        for (int i = 0; i < cardMessage.message.Length; i++)
        {
            //1-indexed��0-indexed�ɕϊ�����
            Sprite sprite = _traderController.CurrentTrader.TraderSprites[cardMessage.face[i] - 1];
            string message = cardMessage.message[i].ToInBracketsText();

            _traderController.SetTraderSprite(sprite);
            try
            {
                await _textBoxController.UpdateText(speaker, message);
            }
            catch (OperationCanceledException)
            {
                return;
            }
        }
    }

    public async UniTask OnBuy(Card card)
    {
        if (card.Id < 0) return;

        string speaker = _traderController.CurrentTrader.Name;
        var buyMessage = _traderController.CurrentTrader.BuyMessage(card);

        //�o��̃e�L�X�g��\������
        for (int i = 0; i < buyMessage.message.Length; i++)
        {
            //1-indexed��0-indexed�ɕϊ�����
            Sprite sprite = _traderController.CurrentTrader.TraderSprites[buyMessage.face[i] - 1];
            string message = buyMessage.message[i].ToInBracketsText();

            _traderController.SetTraderSprite(sprite);
            try
            {
                await _textBoxController.UpdateText(speaker, message);
            }
            catch (OperationCanceledException)
            {
                return;
            }
        }
    }

    public async UniTask OnSell(Card card)
    {
        if (card.Id < 0) return;

        string speaker = _traderController.CurrentTrader.Name;
        var sellMessage = _traderController.CurrentTrader.SellMessage(card);

        //�o��̃e�L�X�g��\������
        for (int i = 0; i < sellMessage.message.Length; i++)
        {
            //1-indexed��0-indexed�ɕϊ�����
            Sprite sprite = _traderController.CurrentTrader.TraderSprites[sellMessage.face[i] - 1];
            string message = sellMessage.message[i].ToInBracketsText();

            _traderController.SetTraderSprite(sprite);
            try
            {
                await _textBoxController.UpdateText(speaker, message);
            }
            catch (OperationCanceledException)
            {
                return;
            }
        }
    }
}
