using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TR1_NormalTrader : Trader
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void CreateDeck(CardPool pool)
    {
        while (true)
        {
            Card card = pool.Draw();
            if (card.Id < 0) break;

            deck.Add(card);
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

        Favorability totalAddValue = traderParameter.AddFavorabilityValue;

        if (traderParameter.FavoriteCards.Contains(selledCard))
        {
            totalAddValue.Add(traderParameter.FavoriteCardBonus);
        }

        favorability = favorability.Add(totalAddValue);
    }

    public override void OnPlayerBuy()
    {
        //買い処理はお気に入りエレメントは関係ない
        favorability = favorability.Add(traderParameter.AddFavorabilityValue);
    }
}
