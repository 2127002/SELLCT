using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TR2_RuinedNobility : Trader
{
    [SerializeField] TraderParameter _traderParameter;
    Favorability _favorability;
    TraderDeck _deck = new();

    public override TraderDeck TraderDeck => _deck;

    public override void CreateDeck(CardPool pool)
    {
        List<Card> list = new();
        list.AddRange(pool.Pool());

        foreach (var item in _traderParameter.PriorityCards())
        {
            for (int i = 0; i < item.Priority; i++)
            {
                list.Add(item.Card);
            }
        }

        for (int i = 0; i < _traderParameter.InitalDeckCount(); i++)
        {
            int index = Random.Range(0, list.Count);
            if (list.Count <= index) break;

            Card card = pool.Draw(list[index]);
            list.RemoveAt(index);

            if (card is EEX_null) break;
            _deck.Add(card);
        }
    }

    public override string BuyMessage(Card card)
    {
        throw new System.NotImplementedException();
    }

    public override string CardMessage(Card card)
    {
        throw new System.NotImplementedException();
    }

    public override string EndMessage()
    {
        throw new System.NotImplementedException();
    }

    public override string SellMessage(Card card)
    {
        throw new System.NotImplementedException();
    }

    public override string StartMessage()
    {
        throw new System.NotImplementedException();
    }
}
