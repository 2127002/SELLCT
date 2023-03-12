using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trader : MonoBehaviour
{
    public abstract TraderDeck TraderDeck { get; }
    public abstract void CreateDeck(CardPool pool);
    public abstract void OnPlayerSell(Card card);
    public abstract void OnPlayerBuy();

    public abstract string StartMessage();
    public abstract string EndMessage();
    public abstract string CardMessage(Card card);
    public abstract string BuyMessage(Card card);
    public abstract string SellMessage(Card card);
}
