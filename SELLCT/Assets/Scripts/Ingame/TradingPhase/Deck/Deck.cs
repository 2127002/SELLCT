using System.Collections.Generic;
using System;

public class Deck
{
    readonly List<ICard> _cards;

    public Deck()
    {
        _cards = new List<ICard>();
    }

    private Deck(List<ICard> cards)
    {
        _cards = cards;
    }

    public Deck Add(ICard card)
    {
        var cards = new List<ICard>(_cards);
        cards.Add(card);
        return new Deck(cards);
    }

    public ICard TakeTopCard()
    {
        if (IsEmpty()) throw new InvalidOperationException("The deck is empty.");

        var card = _cards[0];
        var cards = new List<ICard>(_cards);
        cards.RemoveAt(0);
        return card;
    }

    public bool IsEmpty()
    {
        return _cards.Count == 0;
    }
}