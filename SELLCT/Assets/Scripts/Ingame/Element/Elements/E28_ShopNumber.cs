using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E28_ShopNumber : Card
{
    [SerializeField] Hand _traderHand = default!;
    [SerializeField] PhaseController _phaseController = default!;

    private void Awake()
    {
        _phaseController.OnGameStart.Add(OnGameStart);
    }

    private void OnDestroy()
    {
        _phaseController.OnGameStart.Remove(OnGameStart);
    }

    private void OnGameStart()
    {
        int add = _handMediator.FindAll(this);
        _traderHand.AddHandCapacity(add);

        for (int i = 0; i < add; i++)
        {
            _handMediator.DrawCard();
        }
    }

    public override void Buy()
    {
        _controller.DecreaseMoney(_parameter.GetMoney());
        _traderHand.AddHandCapacity(1);
    }

    public override void Passive()
    {
        // DoNothing
    }

    public override void Sell()
    {
        _controller.IncreaseMoney(_parameter.GetMoney());
        _traderHand.AddHandCapacity(-1);
    }
}
