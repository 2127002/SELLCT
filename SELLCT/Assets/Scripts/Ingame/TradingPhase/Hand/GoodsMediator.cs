using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsMediator : DeckMediator
{
    [SerializeField] Hand _hand = default!;
    [SerializeField] CardUIInstance _cardUIInstance = default!;
    [SerializeField] TraderController _traderController = default!;
    [SerializeField] PhaseController _phaseController = default!;
    [SerializeField] CardUIGenerator _cardUIGenerator = default!;

    //�v���C���[�����p�����J�[�h���ꎞ�I�ɓ���f�b�L
    readonly BuyingCardDeck _buyingCardDeck = new();

    private void Awake()
    {
        _phaseController.OnTradingPhaseComplete.Add(OnComplete);
        _phaseController.OnTradingPhaseStart.Add(InitTakeCard);
    }

    private void OnDestroy()
    {
        _phaseController.OnTradingPhaseComplete.Remove(OnComplete);
        _phaseController.OnTradingPhaseStart.Remove(InitTakeCard);
    }

    private async UniTask OnComplete()
    {
        var token = this.GetCancellationTokenOnDestroy();

        //�w���f�b�L�̃J�[�h�����ׂĎR�D�ɖ߂�
        while (true)
        {
            Card card = _buyingCardDeck.Draw();

            if (card is EEX_null) break;

            _traderController.CurrentTrader.TraderDeck.Add(card);
        }

        //��D�̃J�[�h�����ׂĎR�D�ɖ߂�
        RemoveAllHandCards();

        await UniTask.Yield(token);
    }

    private void RemoveAllHandCards()
    {
        int handCardCount = _hand.Cards.Count;

        for (int i = 0; i < handCardCount; i++)
        {
            Card card = _hand.Cards[0];

            _hand.Remove(card);

            if (card is EEX_null) continue;
            _traderController.CurrentTrader.TraderDeck.Add(card);
        }
    }


    private void InitTakeCard()
    {
        //�L���p�V�e�B��葽�������炻�̕����폜����
        int capacityDifference = _cardUIInstance.Handlers.Count - _hand.Capacity;

        for (int i = 0; i < capacityDifference; i++)
        {
            _cardUIInstance.RemoveAt(_cardUIInstance.Handlers.Count - 1);
        }

        //���ꂩ��h���[����
        int drawableCount = _hand.CalcDrawableCount();

        for (int i = 0; i < drawableCount; i++)
        {
            DrawCard();
        }
    }

    public override void DrawCard()
    {
        if (_hand.CalcDrawableCount() <= 0) return;

        Card top = _traderController.CurrentTrader.TraderDeck.Draw();

        //�h���[����Ƃ���CardUI���Ȃ���΍��o��
        int capacityDifference = _hand.Capacity - _cardUIInstance.Handlers.Count;

        for (int i = 0; i < capacityDifference; i++)
        {
            _cardUIGenerator.Generate();
        }

        //UI�v�f�ɒǉ�
        _cardUIInstance.Handlers[_hand.Cards.Count].SetCardSprites(top);

        //��D�ɒǉ�
        _hand.Add(top);
    }

    public override void RemoveHandCard(Card card)
    {
        _hand.Remove(card);
    }

    public override void AddDeck(Card card)
    {
        _traderController.CurrentTrader.TraderDeck.Add(card);
    }

    public override void UpdeteCardSprites()
    {
        int handCapacity = _hand.Capacity;
        int currentHandCount = _hand.Cards.Count;

        //CardUI���Ȃ���΍��o��
        int capacityDifference = _hand.Capacity - _cardUIInstance.Handlers.Count;

        for (int i = 0; i < capacityDifference; i++)
        {
            _cardUIGenerator.Generate();
        }

        //��D�𔽉f������
        for (int i = 0; i < currentHandCount; i++)
        {
            _cardUIInstance.Handlers[i].SetCardSprites(_hand.Cards[i]);
        }

        //��D���L���p�V�e�B��菭�Ȃ��Ȃ��\���ɂ���
        //�������킹���z�u�ɂ��Ȃ��ŁA�J�[�h���������Ƃ������܂��B
        for (int i = currentHandCount; i < handCapacity; i++)
        {
            _cardUIInstance.Handlers[i].SetCardSprites(EEX_null.Instance);
        }
    }

    public override void AddBuyingDeck(Card card)
    {
        _buyingCardDeck.Add(card);
    }

    public override bool ContainsCard(Card card)
    {
        return _traderController.CurrentTrader.TraderDeck.ContainsCard(card) || _hand.ContainsCard(card) || _buyingCardDeck.ContainsCard(card);
    }

    public override int FindAll(Card card)
    {
        return _traderController.CurrentTrader.TraderDeck.FindAll(card) + _hand.FindAll(card) + _buyingCardDeck.FindAll(card);
    }

    public override Card GetCardAtCardUIHandler(CardUIHandler handler)
    {
        for (int i = 0; i < _cardUIInstance.Handlers.Count; i++)
        {
            //�C���X�^���X����v���Ă����瓯���C���f�b�N�X�ԍ��̎�D��Ԃ�
            if (handler == _cardUIInstance.Handlers[i])
            {
                return _hand.Cards[i];
            }
        }

        throw new System.NullReferenceException("�w�肳�ꂽHandler��������܂���ł����BHandler��Mediator�ɓo�^����Ă��邩�m�F���Ă��������B" + handler);
    }
}
