using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMediator : MonoBehaviour
{
    [SerializeField] Hand _hand;
    [SerializeField] NormalDeck _deck;
    [SerializeField] List<CardUIHandler> _clickHandlers = new();

    public void TakeCard()
    {
        int drawableCount = _hand.CalcDrawableCount();

        for (int i = 0; i < drawableCount; i++)
        {
            ICard top = _deck.TakeTopCard();

            //ŽèŽD‚É’Ç‰Á
            _hand.Add(top);

            //UI—v‘f‚É’Ç‰Á
            _clickHandlers[i].InsertCard(top);
        }
    }
}
