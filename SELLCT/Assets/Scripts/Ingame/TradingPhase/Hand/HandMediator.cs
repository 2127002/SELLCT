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
        //èD§ŒÀ
        int drawableCount = _hand.CalcDrawableCount();

        //èD•â[
        for (int i = 0; i < drawableCount; i++)
        {
            Card top = _deck.TakeTopCard();

            //èD‚É’Ç‰Á
            _hand.Add(top);

            //UI—v‘f‚É’Ç‰Á
            _clickHandlers[i].InsertCard(top);
        }
    }

    public void RearrangeCardSlots()
    {
        int handCapacity = _hand.HandCapacity();

        //‡”Ô“ü‚ê‘Ö‚¦
        for (int i = 0; i < handCapacity - 1; i++)
        {
            if (true == _clickHandlers[i].NullCheck())
            {
                //ƒJ[ƒh–¼æ“¾
                var cardName = _clickHandlers[i + 1].GetCardName();
                _clickHandlers[i + 1].InsertCard(eX_Null);
                _clickHandlers[i].InsertCard(cardName);
            }
        }
    }

    public Card TakeDeckTopCard()
    {
        if (_hand.CalcDrawableCount() == 0) return eX_Null;

        Card top = _deck.TakeTopCard();

        //èD‚É’Ç‰Á
        _hand.Add(top);

        return top;
    }

    public void RemoveHandCard(Card card)
    {
        _hand.Remove(card);
    }
}
