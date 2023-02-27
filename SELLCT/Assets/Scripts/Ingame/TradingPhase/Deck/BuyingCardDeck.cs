using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyingCardDeck : MonoBehaviour
{
    [SerializeField] TimeLimitController _timeLimitController;

    //現在ターンで購入したカードが封入される
    readonly Deck _buyingDeck = new();

    private void Awake()
    {
        _timeLimitController.AddAction(OnTimeLimit);
    }

    public void AddBuyingCard(ICard card)
    {
        _buyingDeck.Add(card);
    }

    private void OnTimeLimit()
    {
        while (!_buyingDeck.IsEmpty())
        {
            _buyingDeck.TakeTopCard();
        }
    }
}
