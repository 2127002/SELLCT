using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TR1_NormalTrader : Trader
{
    [SerializeField] TraderParameter _traderParameter;
    [SerializeField] Sprite _sprite;
    Favorability _favorability;
    readonly TraderDeck _deck = new();

    public override TraderDeck TraderDeck => _deck;

    public override string Name
    {
        get => _traderParameter.Name;
        set => _traderParameter.Name = value;
    }

    public override int InitialDisplayItemCount => _traderParameter.InitialDisplayItemCount;

    public override Sprite Sprite => _sprite;

    private void Awake()
    {
        _favorability = _traderParameter.InitialFavorability;
        _traderParameter.Name = _traderParameter.DefalutName;
    }

    public override void CreateDeck(CardPool pool)
    {
        while (true)
        {
            Card card = pool.Draw();
            if (card is EEX_null) break;

            _deck.Add(card);
        }
    }

    public override string StartMessage()
    {
        return "「奈落における通貨はカラットと呼ばれている」\r\n「あらゆる存在にとって等しく価値のある輝き」\r\n「さて、それは何なんだろうな」";
    }

    public override string EndMessage()
    {
        return "「毎度あり」\r\n「まぁ、また直ぐに会うことになるだろう」";
    }

    public override string CardMessage(Card card)
    {
        return "This Card is a Element of Life. But this message is displayed all Elements";
    }

    public override string BuyMessage(Card card)
    {
        return "Thanks!";
    }

    public override string SellMessage(Card card)
    {
        return "Oh,This Element looks like good!";
    }

    public override void OnPlayerSell(Card selledCard)
    {
        //TODO：もし与えられたカードが仲間カードなら好感度減少処理を行う。
        AddFavorability(selledCard);
    }

    public override void OnPlayerBuy()
    {
        //買い処理はお気に入りエレメントは関係ない
        _favorability = _favorability.Add(_traderParameter.AddFavorabilityValue);
    }

    private void AddFavorability(Card card)
    {
        Favorability totalAddValue = _traderParameter.AddFavorabilityValue;

        if (_traderParameter.FavoriteCards.Contains(card))
        {
            totalAddValue.Add(_traderParameter.FavoriteCardAddValue);
        }

        _favorability = _favorability.Add(totalAddValue);
    }
}
