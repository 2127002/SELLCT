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
        AddFavorability(selledCard);
    }

    public override void OnPlayerBuy()
    {
        //���������͂��C�ɓ���G�������g�͊֌W�Ȃ�
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
