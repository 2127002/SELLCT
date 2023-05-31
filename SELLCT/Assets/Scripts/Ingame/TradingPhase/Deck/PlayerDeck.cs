using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : IDeck
{
    readonly List<Card> _deck = new();
    readonly DeckUIController _deckUIController = default!;

    public PlayerDeck(DeckUIController deckUIController)
    {
        _deckUIController = deckUIController;
    }

    public void Add(Card card)
    {
        _deck.Add(card);
        _deckUIController.ChangeDeckCount(_deck.Count);
    }

    public Card Draw()
    {
        if (_deck.Count == 0) return EEX_null.Instance;

        //ランダムに返す処理をしています。
        //意図的にドローしたい際はこの辺に処理を追加してください。
        //int index = UnityEngine.Random.Range(0, _deck.Count);
        int index = 0;

        Card card = _deck[index];
        _deck.RemoveAt(index);
        _deckUIController.ChangeDeckCount(_deck.Count);

        return card;
    }

    public bool ContainsCard(Card card)
    {
        return _deck.Contains(card);
    }

    public int FindAll(Card card)
    {
        var list = _deck.FindAll(c => c.Id.Equals(card.Id));

        return list.Count;
    }
}
