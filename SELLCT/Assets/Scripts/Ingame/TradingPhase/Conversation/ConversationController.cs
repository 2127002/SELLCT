using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ConversationController : MonoBehaviour
{
    [SerializeField] TraderController _traderController = default!;
    [SerializeField] PlayerMonologue _playerMonologue = default!;
    [SerializeField] TextBoxController _textBoxController = default!;

    const string DEFAULT = "default";

    private void Reset()
    {
        _traderController = FindFirstObjectByType<TraderController>();
        _playerMonologue = FindFirstObjectByType<PlayerMonologue>();
        _textBoxController = FindFirstObjectByType<TextBoxController>();
    }

    public async UniTask OnStart()
    {
        //�v���C���[�̓Ɣ��ɒu�������邩���肷��
        bool switchToPlayerMonologue = _playerMonologue.SwitchToPlayerMonologue;

        string speaker = switchToPlayerMonologue ? _playerMonologue.Speaker : _traderController.CurrentTrader.Name;
        string[] startMessage = switchToPlayerMonologue ? _playerMonologue.StartMessage() : _traderController.CurrentTrader.StartMessage();

        //default�w�肾�����珑��������Default�̕��͂ɏ���������
        if (startMessage[0] == DEFAULT)
        {
            startMessage = _playerMonologue.StartMessage();
        }

        //�o��̃e�L�X�g��\������
        for (int i = 0; i < startMessage.Length; i++)
        {
            await _textBoxController.UpdateText(speaker, startMessage[i].ToInBracketsText());
        }
    }

    public async UniTask OnEnd()
    {
        //�v���C���[�̓Ɣ��ɒu�������邩���肷��
        bool switchToPlayerMonologue = _playerMonologue.SwitchToPlayerMonologue;

        string speaker = switchToPlayerMonologue ? _playerMonologue.Speaker : _traderController.CurrentTrader.Name;
        string[] endMessage = switchToPlayerMonologue ? _playerMonologue.EndMessage() : _traderController.CurrentTrader.EndMessage();

        //default�w�肾�����珑��������Default�̕��͂ɏ���������
        if (endMessage[0] == DEFAULT)
        {
            endMessage = _playerMonologue.EndMessage();
        }

        //�e�L�X�g�̕\��
        for (int i = 0; i < endMessage.Length; i++)
        {
            await _textBoxController.UpdateText(speaker, endMessage[i].ToInBracketsText());
        }
    }

    public async UniTask OnSelect(Card card)
    {
        if (card.Id < 0) return;

        //�v���C���[�̓Ɣ��ɒu�������邩���肷��
        bool switchToPlayerMonologue = _playerMonologue.SwitchToPlayerMonologue;

        string speaker = switchToPlayerMonologue ? _playerMonologue.Speaker : _traderController.CurrentTrader.Name;
        string[] cardMessage = switchToPlayerMonologue ? _playerMonologue.CardMessage(card) : _traderController.CurrentTrader.CardMessage(card);

        //default�w�肾�����珑��������Default�̕��͂ɏ���������
        if (cardMessage[0] == DEFAULT)
        {
            cardMessage = _playerMonologue.CardMessage(card);
        }

        for (int i = 0; i < cardMessage.Length; i++)
        {
            await _textBoxController.UpdateText(speaker, cardMessage[i].ToInBracketsText());
        }
    }

    public async UniTask OnBuy(Card card)
    {
        if (card.Id < 0) return;

        //�v���C���[�̓Ɣ��ɒu�������邩���肷��
        bool switchToPlayerMonologue = _playerMonologue.SwitchToPlayerMonologue;

        string speaker = switchToPlayerMonologue ? _playerMonologue.Speaker : _traderController.CurrentTrader.Name;
        string[] message = switchToPlayerMonologue ? _playerMonologue.BuyMessage(card) : _traderController.CurrentTrader.BuyMessage(card);

        //default�w�肾�����珑��������Default�̕��͂ɏ���������
        if (message[0] == DEFAULT)
        {
            message = _playerMonologue.BuyMessage(card);
        }

        for (int i = 0; i < message.Length; i++)
        {
            await _textBoxController.UpdateText(speaker, message[i].ToInBracketsText());
        }
    }

    public async UniTask OnSell(Card card)
    {
        if (card.Id < 0) return;

        //�v���C���[�̓Ɣ��ɒu�������邩���肷��
        bool switchToPlayerMonologue = _playerMonologue.SwitchToPlayerMonologue;

        string speaker = switchToPlayerMonologue ? _playerMonologue.Speaker : _traderController.CurrentTrader.Name;
        string[] message = switchToPlayerMonologue ? _playerMonologue.SellMessage(card) : _traderController.CurrentTrader.SellMessage(card);

        //default�w�肾�����珑��������Default�̕��͂ɏ���������
        if (message[0] == DEFAULT)
        {
            message = _playerMonologue.SellMessage(card);
        }

        for (int i = 0; i < message.Length; i++)
        {
            await _textBoxController.UpdateText(speaker, message[i].ToInBracketsText());
        }
    }
}
