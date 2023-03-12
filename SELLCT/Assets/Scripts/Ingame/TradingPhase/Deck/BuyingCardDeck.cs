using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyingCardDeck : IDeck
{
    //現在ターンで購入したカードが封入される
    readonly List<Card> _cards = new();

    public void Add(Card card)
    {
        _cards.Add(card);
    }

    public Card Draw()
    {
        if (_cards.Count == 0) return EEX_null.Instance;

        //トップから1枚引く
        Card card = _cards[0];
        _cards.RemoveAt(0);

        return card;
    }
    public bool ContainsCard(Card card)
    {
        return _cards.Contains(card);
    }
}
