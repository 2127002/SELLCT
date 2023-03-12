using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TR1_NormalTrader : Trader
{
    [SerializeField] TraderParameter _traderParameter;
    Favorability _favorability;
    TraderDeck _deck = new();

    public override TraderDeck TraderDeck => _deck;

    private void Awake()
    {
        _favorability = _traderParameter.InitialFavorability();
    }

    public override void CreateDeck(CardPool pool)
    {
        while (true)
        {
            Card card = pool.Draw();
            if (card is EEX_null) break;

            _deck.Add(card);
        }
    }

    public override string StartMessage()
    {
        return "Hello. My name is normal trader.";
    }

    public override string EndMessage()
    {
        throw new System.NotImplementedException();
    }

    public override string CardMessage(Card card)
    {
        throw new System.NotImplementedException();
    }

    public override string BuyMessage(Card card)
    {
        throw new System.NotImplementedException();
    }

    public override string SellMessage(Card card)
    {
        throw new System.NotImplementedException();
    }
}
