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

    public override string[] BuyMessage(Card card)
    {
        int cardId = card.Id;

        return _buy.datas[cardId].Text;
    }

    public override string[] CardMessage(Card card)
    {
        int cardId = card.Id;

        return _select.datas[cardId].Text;
    }

    public override string[] EndMessage()
    {
        List<ConversationData> end = _end.datas.Where(x => x.Likability.Contains((int)favorability.FavorabilityClassification())).ToList();

        int index = Random.Range(0, end.Count());
        return end[index].Text;
    }

    public override string[] SellMessage(Card card)
    {
        int cardId = card.Id;

        return _sell.datas[cardId].Text;
    }

    public override string[] StartMessage()
    {
        List<ConversationData> start = _start.datas.Where(x => x.Likability.Contains((int)favorability.FavorabilityClassification())).ToList();

        int index = Random.Range(0, start.Count());
        return start[index].Text;
    }

    public override void OnPlayerSell(Card selledCard)
    {
        //TODO�F�����^����ꂽ�J�[�h�����ԃJ�[�h�Ȃ�D���x�����������s���B

        Favorability totalAddValue = traderParameter.AddFavorabilityValue;

        if (traderParameter.FavoriteCards.Contains(selledCard))
        {
            totalAddValue.Add(traderParameter.FavoriteCardBonus);
        }

        favorability = favorability.Add(totalAddValue);
    }

    public override void OnPlayerBuy()
    {
        //���������͂��C�ɓ���G�������g�͊֌W�Ȃ�
        favorability = favorability.Add(traderParameter.AddFavorabilityValue);
    }
}
