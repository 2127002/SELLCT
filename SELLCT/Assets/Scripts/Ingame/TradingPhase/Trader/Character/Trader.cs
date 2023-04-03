using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trader : MonoBehaviour, IConversationMessage
{
    [SerializeField] protected TraderParameter traderParameter;
    [SerializeField] protected Sprite sprite;
    protected Favorability favorability;
    protected readonly TraderDeck deck = new();

    protected virtual void Awake()
    {
        favorability = traderParameter.InitialFavorability;
        traderParameter.Name = traderParameter.DefaultName;
    }

    public virtual TraderDeck TraderDeck => deck;
    public virtual string Name
    {
        get => traderParameter.Name;
        set
        {
            if (string.IsNullOrEmpty(value)) value = "No name";
            traderParameter.Name = value;
        }
    }
    public virtual int InitialDisplayItemCount => traderParameter.InitialDisplayItemCount;
    public virtual Sprite TraderSprite => sprite;
    public abstract void CreateDeck(CardPool pool);
    public abstract void OnPlayerSell(Card card);
    public abstract void OnPlayerBuy();

    public abstract string StartMessage();
    public abstract string EndMessage();
    public abstract string CardMessage(Card card);
    public abstract string BuyMessage(Card card);
    public abstract string SellMessage(Card card);
}
