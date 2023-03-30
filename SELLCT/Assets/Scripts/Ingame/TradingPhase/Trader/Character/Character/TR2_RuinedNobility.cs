using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TR2_RuinedNobility : Trader
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
        List<Card> list = new();
        list.AddRange(pool.Pool());

        foreach (var item in _traderParameter.PriorityCards)
        {
            for (int i = 0; i < item.Priority; i++)
            {
                list.Add(item.Card);
            }
        }

        for (int i = 0; i < _traderParameter.InitalDeckCount; i++)
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
