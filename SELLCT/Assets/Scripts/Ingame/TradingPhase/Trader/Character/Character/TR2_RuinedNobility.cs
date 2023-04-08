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

            //���������J�[�h��pool�̍Ō�̃J�[�h�Ȃ�������I����Ȃ��悤�ɂ���
            if (!pool.Pool().Contains(card))
            {
                list.RemoveAll(x => x == card);
            }

            if (card.Id < 0) break;
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

        //�D���x�㏸����
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
