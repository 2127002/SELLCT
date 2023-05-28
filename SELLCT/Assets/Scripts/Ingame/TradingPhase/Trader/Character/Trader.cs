using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public readonly struct ConversationMessage
{
    public readonly string[] message;
    public readonly int[] face;
    public readonly string[] name;

    public ConversationMessage(string[] message, int[] face, string[] name)
    {
        if (message.Length != face.Length) throw new NotImplementedException("メッセージと立ち絵の指定個数が異なります。設定を見直してください" + message[0]);

        this.message = message;
        this.face = face;

        //名前の登録がなければ空白文字で埋める
        if (name.Length == 0 && name.Length != message.Length) name = new string[message.Length];
        this.name = name;
    }
}

[Serializable]
public struct TraderOffset
{
    /// <summary>
    /// デフォルト画像の大きさからの拡大比率
    /// </summary>
    [SerializeField] Vector2 scaleRatio;

    [SerializeField] Vector3 offsetPos;

    public Vector2 ScaleRatio => scaleRatio;
    public Vector3 OffsetPos => offsetPos;
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
    [SerializeField] private TraderOffset _offset;

    protected Favorability favorability;
    protected readonly TraderDeck deck = new();
    public int[] CardCount { get; } = new int[64];

    protected virtual void Awake()
    {
        favorability = traderParameter.InitialFavorability;
        traderParameter.Name = traderParameter.DefaultName;
    }

    public virtual TraderOffset Offset => _offset;
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
