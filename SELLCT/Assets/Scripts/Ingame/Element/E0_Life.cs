using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class E0_Life : Card
{
    [SerializeField] CardParameter _parameter = default!;
    [SerializeField] MoneyPossessedController _controller = default!;

    public override int Rarity => _parameter.Rarity();

    public override void Buy()
    {
        //TODO:SE301の再生
        //TODO:画面全体を脈動させるアニメーション

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