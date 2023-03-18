using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderDeck : IDeck
{
    readonly List<Card> _cards = new(); // 山札に含まれるカードのリスト

    public void Add(Card card)
    {
        _cards.Add(card);
    }

    /// <summary>
    /// カードを1枚引く
    /// </summary>
    /// <returns>引いたカード</returns>
    public Card Draw()
    {
        int totalRarity = 0;

        foreach (Card card in _cards)
        {
            totalRarity += card.Rarity;
        }

        int randomNumber = Random.Range(0, totalRarity);
        int cumulativeRarity = 0;

        foreach (Card card in _cards)
        {
            cumulativeRarity += card.Rarity;
            if (randomNumber <= cumulativeRarity)
            {
                _cards.Remove(card);
                return card;
            }
        }

        return EEX_null.Instance;
    }

    /// <summary>
    /// E37を所持していた際のドロー
    /// </summary>
    /// <returns></returns>
    public Card LackDraw()
    {
        int index = Random.Range(0, _cards.Count);

        if (_cards.Count == 0) return EEX_null.Instance;

        Card card = _cards[index];
        _cards.Remove(card);

        return card;
    }

    public bool ContainsCard(Card card)
    {
        return _cards.Contains(card);
    }

    public int FindAll(Card card)
    {
        var list = _cards.FindAll(c => c.Equals(card));

        return list.Count;
    }
}
