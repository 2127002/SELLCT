using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyingCardDeck : MonoBehaviour
{
    [SerializeField] TradingPhaseCompletionHandler _completionHandler;
    [SerializeField] NormalDeck _normalDeck;

    //現在ターンで購入したカードが封入される
    readonly Deck _buyingDeck = new();

    private void Awake()
    {
        _completionHandler.AddListener(OnComplete);
    }

    private void OnDestroy()
    {
        _completionHandler.RemoveListener(OnComplete);
    }

    public void AddBuyingCard(Card card)
    {
        _buyingDeck.Add(card);
    }

    private void OnComplete()
    {
        while (!_buyingDeck.IsEmpty())
        {
            _normalDeck.AddCard(_buyingDeck.TakeTopCard());
        }
    }
}
