using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMediator : DeckMediator
{
    [SerializeField] Hand _hand = default!;
    [SerializeField] CardUIInstance _cardUIInstance = default!;
    [SerializeField] PhaseController _phaseController = default!;
    [SerializeField] DeckUIController _deckUIController = default!;

    //プレイヤーのデッキ
    PlayerDeck _playerDeck;

    public override int[] CardCount => base.CardCount;

    //購入したカードが一時的に入るデッキ
    readonly BuyingCardDeck _buyingCardDeck = new();

    private void Awake()
    {
        _playerDeck = new(_deckUIController);

        _phaseController.OnTradingPhaseComplete.Add(OnComplete);
        _phaseController.OnTradingPhaseStart.Add(InitTakeCard);
    }

    private void OnDestroy()
    {
        _phaseController.OnTradingPhaseComplete.Remove(OnComplete);
        _phaseController.OnTradingPhaseStart.Remove(InitTakeCard);
    }

    //売買フェーズ終了時に行う処理。
    private async UniTask OnComplete()
    {
        var token = this.GetCancellationTokenOnDestroy();

        //購入デッキのカードをすべて山札に戻す
        while (true)
        {
            Card card = _buyingCardDeck.Draw();

            //限界までドローしたらNULL相当が来るためそれを検知する
            if (card.Id < 0) break;

            _playerDeck.Add(card);
        }

        //手札のカードをすべて山札に戻す
        RemoveAllHandCards();

        await UniTask.Yield(token);
    }

    private void RemoveAllHandCards()
    {
        int currentHandCount = _hand.Cards.Count;

        for (int i = 0; i < currentHandCount; i++)
        {
            Card card = _hand.Cards[0];

            _hand.Remove(card);
            _playerDeck.Add(card);
        }
    }

    private void InitTakeCard()
    {
        //引ける枚数を取得
        int drawableCount = _hand.CalcDrawableCount();

        //手札補充
        for (int i = 0; i < drawableCount; i++)
        {
            DrawCard();
        }
    }

    public override void UpdateCardSprites()
    {
        int handCapacity = _hand.Capacity;
        int currentHandCount = _hand.Cards.Count;

        //手札を反映させる
        for (int i = 0; i < currentHandCount; i++)
        {
            _cardUIInstance.Handlers[i].SetCardSprites(_hand.Cards[i]);
        }

        //手札がキャパシティより少ないなら非表示にする
        //数を合わせた配置にしないで、カードが無いことを示します。
        for (int i = currentHandCount; i < handCapacity; i++)
        {
            _cardUIInstance.Handlers[i].SetCardSprites(EEX_null.Instance);
        }
    }

    public override void DrawCard()
    {
        if (_hand.CalcDrawableCount() <= 0) return;

        Card card = _playerDeck.Draw();

        //すでに手札にあるなら加えない
        if (_hand.ContainsCard(card)) return;

        //手札に追加
        _hand.Add(card);

        //UIをセットする
        _cardUIInstance.Handlers[_hand.Cards.Count - 1].SetCardSprites(card);
    }

    public override bool RemoveHandCard(Card card)
    {
        //所持カード枚数を減らす
        CardCount[card.Id]--;
        if (CardCount[card.Id] > 0)
        {
            return false;
        }

        return _hand.Remove(card);
    }

    public override void AddPlayerDeck(Card card)
    {
        //所持カード枚数を増やす
        CardCount[card.Id]++;

        _playerDeck.Add(card);
    }

    public override void AddBuyingDeck(Card card)
    {
        //所持カード枚数を増やす
        CardCount[card.Id]++;

        _buyingCardDeck.Add(card);
    }

    public override bool ContainsCard(Card card)
    {
        return CardCount[card.Id] > 0;
    }

    public override int FindAll(Card card)
    {
        return CardCount[card.Id];
    }

    public override Card GetCardAtCardUIHandler(CardUIHandler handler)
    {
        for (int i = 0; i < _cardUIInstance.Handlers.Count; i++)
        {
            //インスタンスが一致していたら同じインデックス番号の手札を返す
            if (handler == _cardUIInstance.Handlers[i])
            {
                return _hand.Cards[i];
            }
        }

        throw new System.NullReferenceException("指定されたHandlerが見つかりませんでした。HandlerがMediatorに登録されているか確認してください。" + handler);
    }
}