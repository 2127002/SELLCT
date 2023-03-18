using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMediator : DeckMediator
{
    [SerializeField] Hand _hand = default!;
    [SerializeField] CardUIInstance _cardUIInstance = default!;
    [SerializeField] PhaseController _phaseController = default!;

    //プレイヤーのデッキ
    readonly PlayerDeck _playerDeck = new();

    //購入したカードが一時的に入るデッキ
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

    //売買フェーズ終了時に行う処理。
    private async UniTask OnComplete()
    {
        var token = this.GetCancellationTokenOnDestroy();

        //購入デッキのカードをすべて山札に戻す
        while (true)
        {
            Card card = _buyingCardDeck.Draw();

            if (card is EEX_null) break;

            _playerDeck.Add(card);
        }

        //手札のカードをすべて山札に戻す
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

    public override void UpdeteCardSprites()
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
        if (_hand.CalcDrawableCount() == 0) return;

        Card card = _playerDeck.Draw();

        //UIをセットする
        _cardUIInstance.Handlers[_hand.Cards.Count].SetCardSprites(card);

        //手札に追加
        _hand.Add(card);
    }

    public override void RemoveHandCard(Card card)
    {
        _hand.Remove(card);
    }

    public override void AddDeck(Card card)
    {
        _playerDeck.Add(card);
    }

    public override void AddBuyingDeck(Card card)
    {
        _buyingCardDeck.Add(card);
    }

    public override bool ContainsCard(Card card)
    {
        return _playerDeck.ContainsCard(card) || _hand.ContainsCard(card) || _buyingCardDeck.ContainsCard(card);
    }

    public override int FindAll(Card card)
    {
        return _playerDeck.FindAll(card) + _hand.FindAll(card) + _buyingCardDeck.FindAll(card);
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