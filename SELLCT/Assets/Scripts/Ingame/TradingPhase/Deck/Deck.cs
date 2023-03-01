using System.Collections.Generic;
using System;

public class Deck
{
    readonly List<ICard> _cards;

    public Deck()
    {
        _cards = new List<ICard>();
    }

    public void Add(ICard card)
    {
        _cards.Add(card);
    }

    /// <exception cref="InvalidOperationException"></exception>
    public ICard TakeTopCard()
    {
        if (IsEmpty()) throw new InvalidOperationException("The deck is empty.");

        var card = _cards[0];
        _cards.RemoveAt(0);
        return card;
    }

    public bool IsEmpty()
    {
        return _cards.Count == 0;
    }
}