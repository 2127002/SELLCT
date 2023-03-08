using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalDeck : MonoBehaviour
{
    Deck _deck = new();

    [SerializeField] EEX_null exNull;

    public void AddCard(Card card)
    {
        _deck.Add(card);
    }

    public Card TakeTopCard()
    {
        Card card;

        try
        {
            card = _deck.TakeTopCard();
        }
        catch (InvalidOperationException)
        {
            //null�ɑ�������J�[�h�B
            return exNull;
        }

        return card;
    }
}
