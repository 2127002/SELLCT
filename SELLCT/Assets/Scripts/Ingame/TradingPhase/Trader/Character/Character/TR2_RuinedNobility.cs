using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TR2_RuinedNobility : Trader
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void CreateDeck(CardPool pool)
    {
        List<Card> list = new();
        list.AddRange(pool.Pool());

        foreach (var item in traderParameter.PriorityCards)
        {
            for (int i = 0; i < item.Priority; i++)
            {
                list.Add(item.Card);
            }
        }

        for (int i = 0; i < traderParameter.InitalDeckCount; i++)
        {
            int index = Random.Range(0, list.Count);
            if (list.Count <= index) break;

            Card card = pool.Draw(list[index]);
            list.RemoveAt(index);

            //今引いたカードがpoolの最後のカードならもう抽選されないようにする
            if (!pool.Pool().Contains(card))
            {
                list.RemoveAll(x => x == card);
            }

            if (card.Id < 0) break;
            deck.Add(card);
        }
    }

    public override string BuyMessage(Card card)
    {
        throw new System.NotImplementedException();
    }

    public override string CardMessage(Card card)
    {
        throw new System.NotImplementedException();
    }

    public override string EndMessage()
    {
        return "「時は宝石なり」\r\n「誰の言葉かって？」\r\n「俺の言葉だよ」";
    }

    public override string SellMessage(Card card)
    {
        throw new System.NotImplementedException();
    }

    public override string StartMessage()
    {
        return "「ここは随分と暗いな」\r\n「けれど、暗闇でこそ輝くものに価値はある」\r\n「さて、今日はどんな宝石を持ってきたんだ？」";
    }

    public override void OnPlayerSell(Card selledCard)
    {
        //TODO：もし与えられたカードが仲間カードなら好感度減少処理を行う。

        //好感度上昇処理
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
