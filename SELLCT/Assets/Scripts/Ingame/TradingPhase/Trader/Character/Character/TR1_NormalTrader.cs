using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows;

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
            CardCount[card.Id]++;
            deck.Add(card);
        }
    }

    public override ConversationMessage BuyMessage(Card card)
    {
        List<ConversationData> data = _buy.datas.Where(x => x.Likability.Contains((int)favorability.FavorabilityClassification())).ToList();

        int cardId = card.Id;

        string[] texts = data[cardId].Text;
        int[] face = data[cardId].Face;
        string[] name = data[cardId].Name;

        return new(texts, face, name);
    }

    public override ConversationMessage CardMessage(Card card)
    {
        List<ConversationData> data = _select.datas.Where(x => x.Likability.Contains((int)favorability.FavorabilityClassification())).ToList();

        int cardId = card.Id;

        string[] texts = data[cardId].Text;
        int[] face = data[cardId].Face;
        string[] name = data[cardId].Name;

        return new(texts, face, name);
    }

    public override ConversationMessage EndMessage()
    {
        List<ConversationData> end = _end.datas.Where(x => x.Likability.Contains((int)favorability.FavorabilityClassification())).ToList();

        int index = Random.Range(0, end.Count());

        string[] texts = end[index].Text;
        int[] face = end[index].Face;
        string[] name = end[index].Name;

        return new(texts, face, name);
    }

    public override ConversationMessage SellMessage(Card card)
    {
        List<ConversationData> data = _sell.datas.Where(x => x.Likability.Contains((int)favorability.FavorabilityClassification())).ToList();

        int cardId = card.Id;

        string[] texts = data[cardId].Text;
        int[] face = data[cardId].Face;
        string[] name = data[cardId].Name;

        return new(texts, face, name);
    }

    public override ConversationMessage StartMessage()
    {
        List<ConversationData> start = _start.datas.Where(x => x.Likability.Contains((int)favorability.FavorabilityClassification())).ToList();

        int index = Random.Range(0, start.Count());

        string[] texts = start[index].Text;
        int[] face = start[index].Face;
        string[] name = start[index].Name;

        return new(texts, face, name);
    }

    public override ConversationMessage SceneEndingMessage(EndingController.EndingScene endingScene)
    {
        List<ConversationData> data = _sceneEnding.datas.Where(x => x.Likability.Contains((int)favorability.FavorabilityClassification())).ToList();

        int index = (int)endingScene + 1;

        string[] texts = data[index].Text;
        int[] face = data[index].Face;
        string[] name = data[index].Name;

        return new(texts, face, name);
    }

    public override void OnPlayerSell(Card selledCard)
    {
        Favorability totalAddValue = traderParameter.AddFavorabilityValue;

        if (traderParameter.FavoriteCards.Contains(selledCard))
        {
            totalAddValue = totalAddValue.Add(traderParameter.FavoriteCardBonus);
        }

        favorability = favorability.Add(totalAddValue);
    }

    public override void OnPlayerBuy(Card card)
    {
        Favorability totalAddValue = traderParameter.AddFavorabilityValue;

        if (traderParameter.FavoriteCards.Contains(card))
        {
            totalAddValue = totalAddValue.Add(traderParameter.FavoriteCardBonus);
        }

        favorability = favorability.Add(totalAddValue);
    }
}
