using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

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
        var startMessage = _traderController.CurrentTrader.StartMessage();

        //�o��̃e�L�X�g��\������
        for (int i = 0; i < startMessage.message.Length; i++)
        {
            //1-indexed��0-indexed�ɕϊ�����
            Sprite sprite = _traderController.CurrentTrader.TraderSprites[startMessage.face[i] - 1];

            string speaker = startMessage.name[i];
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
        var endMessage = _traderController.CurrentTrader.EndMessage();

        //�o��̃e�L�X�g��\������
        for (int i = 0; i < endMessage.message.Length; i++)
        {
            //1-indexed��0-indexed�ɕϊ�����
            Sprite sprite = _traderController.CurrentTrader.TraderSprites[endMessage.face[i] - 1];
            string speaker = endMessage.name[i];
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

        var cardMessage = _traderController.CurrentTrader.CardMessage(card);

        //�o��̃e�L�X�g��\������
        for (int i = 0; i < cardMessage.message.Length; i++)
        {
            //1-indexed��0-indexed�ɕϊ�����
            Sprite sprite = _traderController.CurrentTrader.TraderSprites[cardMessage.face[i] - 1];
            string speaker = cardMessage.name[i];
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

        var buyMessage = _traderController.CurrentTrader.BuyMessage(card);

        //�o��̃e�L�X�g��\������
        for (int i = 0; i < buyMessage.message.Length; i++)
        {
            //1-indexed��0-indexed�ɕϊ�����
            Sprite sprite = _traderController.CurrentTrader.TraderSprites[buyMessage.face[i] - 1];
            string speaker = buyMessage.name[i];
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

        var sellMessage = _traderController.CurrentTrader.SellMessage(card);

        //�o��̃e�L�X�g��\������
        for (int i = 0; i < sellMessage.message.Length; i++)
        {
            //1-indexed��0-indexed�ɕϊ�����
            Sprite sprite = _traderController.CurrentTrader.TraderSprites[sellMessage.face[i] - 1];
            string speaker = sellMessage.name[i];
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

    public async UniTask OnSceneEnding(EndingController.EndingScene endingScene)
    {
        var sceneEndMessage = _traderController.CurrentTrader.SceneEndingMessage(endingScene);

        //�o��̃e�L�X�g��\������
        for (int i = 0; i < sceneEndMessage.message.Length; i++)
        {
            //1-indexed��0-indexed�ɕϊ�����
            Sprite sprite = _traderController.CurrentTrader.TraderSprites[sceneEndMessage.face[i] - 1];
            string speaker = sceneEndMessage.name[i];
            string message = sceneEndMessage.message[i].ToInBracketsText();

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
