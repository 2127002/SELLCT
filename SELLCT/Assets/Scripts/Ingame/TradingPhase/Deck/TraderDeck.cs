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
}
