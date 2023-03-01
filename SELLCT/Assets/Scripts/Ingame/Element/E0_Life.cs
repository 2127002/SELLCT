using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class E0_Life : MonoBehaviour, ICard
{
    [SerializeField] CardParameter _parameter;
    [SerializeField] MoneyPossessedController _controller;
    [SerializeField] NormalDeck _normalDeck;

    private void Awake()
    {
        AddCardToDeck();
    }

    public void AddCardToDeck()
    {
        for (int i = 0; i < _parameter.GetInitialCardCount(); i++)
        {
            _normalDeck.AddCard(this);
        }
    }

    public void Buy()
    {
        //TODO:SE301�̍Đ�
        //TODO:��ʑS�̂𖬓�������A�j���[�V����

        _controller.DecreaseMoney(_parameter.GetMoney());
    }

    public void Passive()
    {
        // DoNothing
    }

    public void Sell()
    {
        _controller.IncreaseMoney(_parameter.GetMoney());
    }
}