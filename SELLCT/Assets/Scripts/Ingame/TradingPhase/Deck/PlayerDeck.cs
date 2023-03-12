using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : IDeck
{
    List<Card> _deck = new();

    public void Add(Card card)
    {
        _deck.Add(card);
    }

    public Card Draw()
    {
        if (_deck.Count == 0) return EEX_null.Instance;

        //ランダムに返す処理をしています。
        //意図的にドローしたい際はこの辺に処理を追加してください。
        int index = UnityEngine.Random.Range(0, _deck.Count);

        Card card = _deck[index];
        _deck.RemoveAt(index);

        return card;
    }
    public bool ContainsCard(Card card)
    {
        return _deck.Contains(card);
    }
}
