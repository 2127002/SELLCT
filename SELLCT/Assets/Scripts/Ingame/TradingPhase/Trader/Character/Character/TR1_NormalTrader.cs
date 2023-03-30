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
        return "�u�ޗ��ɂ�����ʉ݂̓J���b�g�ƌĂ΂�Ă���v\r\n�u�����鑶�݂ɂƂ��ē��������l�̂���P���v\r\n�u���āA����͉��Ȃ񂾂낤�ȁv";
    }

    public override string EndMessage()
    {
        return "�u���x����v\r\n�u�܂��A�܂������ɉ���ƂɂȂ邾�낤�v";
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
