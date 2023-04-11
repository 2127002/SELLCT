using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class E7_Move : Card
{
    [SerializeField] TradingNextButtonController _tradingNextButtonController = default!;
    [SerializeField] ExplorationNextButtonController _explorationNextButtonController = default!;

    public override int Id => 7;

    public override void Buy()
    {
        OnBuy();
    }

    public override void OnPressedU6Button()
    {
        throw new System.NotImplementedException();
    }

    public override void Sell()
    {
        OnSell();
    }

    private void OnBuy()
    {
        _explorationNextButtonController.Enable();
        _tradingNextButtonController.Enable();
    }

    private void OnSell()
    {
        if (_handMediator.ContainsCard(this)) return;

        _explorationNextButtonController.Disable();
        _tradingNextButtonController.Disable();
    }
}
