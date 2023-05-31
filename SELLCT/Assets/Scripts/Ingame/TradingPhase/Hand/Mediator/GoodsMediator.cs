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
    [SerializeField] E37_Lack _lack = default!;

    //プレイヤーが売却したカードが一時的に入るデッキ
    readonly BuyingCardDeck _buyingCardDeck = new();

    public override int[] CardCount => _traderController.CurrentTrader.CardCount;

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

        //購入デッキのカードをすべて山札に戻す
        while (true)
        {
            Card card = _buyingCardDeck.Draw();

            if (card.Id < 0) break;

            _traderController.CurrentTrader.TraderDeck.Add(card);
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

            if (card.Id < 0) continue;
            _traderController.CurrentTrader.TraderDeck.Add(card);
        }
    }

    private void InitTakeCard()
    {
        //キャパシティより多かったらその分を削除する
        int capacityDifference = _cardUIInstance.Handlers.Count - _hand.Capacity;

        for (int i = 0; i < capacityDifference; i++)
        {
            _cardUIInstance.RemoveAt(_cardUIInstance.Handlers.Count - 1);
        }

        //それからドローする
        int drawableCount = _hand.CalcDrawableCount();

        for (int i = 0; i < drawableCount; i++)
        {
            DrawCard();
        }

        //提示商品枚数より残り山札のほうが少なかったら、残ったHandlerにNull相当を入れて表示を消す。
        //Handler自体を消さないのは商人山札が無いことを示すためです。
        for (int i = drawableCount; i < _cardUIInstance.Handlers.Count; i++)
        {
            _cardUIInstance.Handlers[i].SetCardSprites(EEX_null.Instance);
        }
    }

    public override void DrawCard()
    {
        if (_hand.CalcDrawableCount() <= 0) return;

        Card card;
        if (_lack.ContainsPlayerDeck)
        {
            card = _traderController.CurrentTrader.TraderDeck.LuckDraw();
        }
        else
        {
            card = _traderController.CurrentTrader.TraderDeck.Draw();
        }

        if (card.Id < 0)
        {
            //null相当だった場合以降のカード情報を消したいためすべてのカードにアクセスする
            UpdateCardSprites();
            return;
        }

        //すでに手札にあるなら加えない
        if (_hand.ContainsCard(card)) return;

        //ドローするときにCardUIがなければ作り出す
        int capacityDifference = _hand.Capacity - _cardUIInstance.Handlers.Count;

        for (int i = 0; i < capacityDifference; i++)
        {
            _cardUIGenerator.Generate();
        }

        //手札に追加
        _hand.Add(card);

        //UI要素に追加
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
        CardCount[card.Id]++;

        _traderController.CurrentTrader.TraderDeck.Add(card);
    }

    public override void UpdateCardSprites()
    {
        int currentHandCount = _hand.Cards.Count;

        //CardUIがなければ作り出す
        int capacityDifference = _hand.Capacity - _cardUIInstance.Handlers.Count;

        for (int i = 0; i < capacityDifference; i++)
        {
            _cardUIGenerator.Generate();
        }

        //手札を反映させる
        for (int i = 0; i < currentHandCount; i++)
        {
            _cardUIInstance.Handlers[i].SetCardSprites(_hand.Cards[i]);
        }

        //手札がキャパシティより少ないなら非表示にする
        //数を合わせた配置にしないで、カードが無いことを示します。
        for (int i = currentHandCount; i < _cardUIInstance.Handlers.Count; i++)
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
                if (_hand.Cards.Count <= i) return EEX_null.Instance;
                return _hand.Cards[i];
            }
        }

        throw new System.NullReferenceException("指定されたHandlerが見つかりませんでした。HandlerがMediatorに登録されているか確認してください。" + handler);
    }
}
