using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalDeck : MonoBehaviour
{
    Deck _deck = new();

    [SerializeField] EEX_null exNull;

    public void AddCard(ICard card)
    {
        _deck.Add(card);
    }

    public ICard TakeTopCard()
    {
        ICard card;

        try
        {
            card = _deck.TakeTopCard();
        }
        catch (InvalidOperationException)
        {
            //nullに相当するカード。
            return exNull;
        }

        return card;
    }
}
