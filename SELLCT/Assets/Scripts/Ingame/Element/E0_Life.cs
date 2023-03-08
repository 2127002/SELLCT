using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class E0_Life : Card
{
    [SerializeField] CardParameter _parameter;
    [SerializeField] MoneyPossessedController _controller;
    [SerializeField] NormalDeck _normalDeck;

    private void Awake()
    {
        AddCardToDeck();
    }

    public override void AddCardToDeck()
    {
        for (int i = 0; i < _parameter.GetInitialCardCount(); i++)
        {
            _normalDeck.AddCard(this);
        }
    }

    public override void Buy()
    {
        //TODO:SE301�̍Đ�
        //TODO:��ʑS�̂𖬓�������A�j���[�V����

        _controller.DecreaseMoney(_parameter.GetMoney());
    }

    public override void Passive()
    {
        // DoNothing
    }

    public override void Sell()
    {
        _controller.IncreaseMoney(_parameter.GetMoney());
    }
}