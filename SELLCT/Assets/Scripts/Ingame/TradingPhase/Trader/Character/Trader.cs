using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public readonly struct ConversationMessage
{
    public readonly string[] message;
    public readonly int[] face;

    public ConversationMessage(string[] message, int[] face)
    {
        if (message.Length != face.Length) throw new NotImplementedException("���b�Z�[�W�ƕ\����̌����قȂ�܂��B�ݒ���������Ă��������B" + message);

        this.message = message;
        this.face = face;
    }
}

public abstract class Trader : MonoBehaviour, IConversationMessage
{
    [SerializeField] protected TraderParameter traderParameter = default!;
    [SerializeField] protected Sprite[] sprites;
    [SerializeField] protected ConversationDataBase _start;
    [SerializeField] protected ConversationDataBase _end;
    [SerializeField] protected ConversationDataBase _select;
    [SerializeField] protected ConversationDataBase _buy;
    [SerializeField] protected ConversationDataBase _sell;

    protected Favorability favorability;
    protected readonly TraderDeck deck = new();
    public int[] CardCount { get; } = new int[64];

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
    public virtual Sprite[] TraderSprites => sprites;
    public abstract void CreateDeck(CardPool pool);
    public abstract void OnPlayerSell(Card card);
    public abstract void OnPlayerBuy();

    public abstract ConversationMessage StartMessage();
    public abstract ConversationMessage EndMessage();
    public abstract ConversationMessage CardMessage(Card card);
    public abstract ConversationMessage BuyMessage(Card card);
    public abstract ConversationMessage SellMessage(Card card);
}
