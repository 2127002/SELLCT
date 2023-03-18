using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TR2_RuinedNobility : Trader
{
    [SerializeField] TraderParameter _traderParameter;
    Favorability _favorability;
    TraderDeck _deck = new();

    public override TraderDeck TraderDeck => _deck;
    private void Awake()
    {
        _favorability = _traderParameter.InitialFavorability();
    }

    public override void CreateDeck(CardPool pool)
    {
        List<Card> list = new();
        list.AddRange(pool.Pool());

        foreach (var item in _traderParameter.PriorityCards())
        {
            for (int i = 0; i < item.Priority; i++)
            {
                list.Add(item.Card);
            }
        }

        for (int i = 0; i < _traderParameter.InitalDeckCount(); i++)
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

            if (card is EEX_null) break;
            _deck.Add(card);
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
        return "See you...";
    }

    public override string SellMessage(Card card)
    {
        throw new System.NotImplementedException();
    }

    public override string StartMessage()
    {
        return "Hello. My name is TR2 deGozaru.";
    }

    public override void OnPlayerSell(Card selledCard)
    {
        //TODO�F�����^����ꂽ�J�[�h�����ԃJ�[�h�Ȃ�D���x�����������s���B

        AddFavorability(selledCard);
    }

    public override void OnPlayerBuy()
    {
        //���������͂��C�ɓ���G�������g�͊֌W�Ȃ�
        _favorability = _favorability.Add(_traderParameter.AddFavorabilityValue());
    }

    private void AddFavorability(Card card)
    {
        Favorability totalAddValue = _traderParameter.AddFavorabilityValue();

        if (_traderParameter.FavoriteCards().Contains(card))
        {
            totalAddValue.Add(_traderParameter.FavoriteCardAddValue());
        }

        _favorability = _favorability.Add(totalAddValue);
    }
}
