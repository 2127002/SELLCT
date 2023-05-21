using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TR7_Beast : Trader
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
            CardCount[card.Id]++;
            deck.Add(card);
        }
    }

    public override ConversationMessage BuyMessage(Card card)
    {
        int cardId = card.Id;

        string[] texts = _buy.datas[cardId].Text;
        int[] face = _buy.datas[cardId].Face;

        return new(texts, face);
    }

    public override ConversationMessage CardMessage(Card card)
    {
        int cardId = card.Id;

        string[] texts = _select.datas[cardId].Text;
        int[] face = _select.datas[cardId].Face;

        return new(texts, face);
    }

    public override ConversationMessage EndMessage()
    {
        List<ConversationData> end = _end.datas.Where(x => x.Likability.Contains((int)favorability.FavorabilityClassification())).ToList();

        int index = Random.Range(0, end.Count());

        string[] texts = end[index].Text;
        int[] face = end[index].Face;

        return new(texts, face);
    }

    public override ConversationMessage SellMessage(Card card)
    {
        int cardId = card.Id;

        string[] texts = _sell.datas[cardId].Text;
        int[] face = _sell.datas[cardId].Face;

        return new(texts, face);
    }

    public override ConversationMessage StartMessage()
    {
        List<ConversationData> start = _start.datas.Where(x => x.Likability.Contains((int)favorability.FavorabilityClassification())).ToList();

        int index = Random.Range(0, start.Count());

        string[] texts = start[index].Text;
        int[] face = start[index].Face;

        return new(texts, face);
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

