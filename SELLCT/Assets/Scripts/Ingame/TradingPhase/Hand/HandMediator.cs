using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMediator : MonoBehaviour
{
    [SerializeField] Hand _hand;
    [SerializeField] NormalDeck _deck;
    [SerializeField] EEX_null eX_Null;
    [SerializeField] List<CardUIHandler> _clickHandlers = new();

    public void InitTakeCard()
    {
        int drawableCount = _hand.CalcDrawableCount();

        for (int i = 0; i < drawableCount; i++)
        {
            Card top = _deck.TakeTopCard();

            //手札に追加
            _hand.Add(top);

            //UI要素に追加
            _clickHandlers[i].InsertCard(top);
        }
    }

    public Card TakeDeckTopCard()
    {
        if (_hand.CalcDrawableCount() == 0) return eX_Null;

        Card top = _deck.TakeTopCard();

        //手札に追加
        _hand.Add(top);

        return top;
    }

    public void RemoveHandCard(Card card)
    {
        _hand.Remove(card);
    }
}
