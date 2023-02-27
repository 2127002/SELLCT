using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class E0_Life : MonoBehaviour, ICard
{
    [SerializeField] CardParameter _parameter;
    [SerializeField] MoneyPossessedController _controller;

    public void Buy()
    {
        //TODO:SE301の再生
        //TODO:画面全体を脈動させるアニメーション

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