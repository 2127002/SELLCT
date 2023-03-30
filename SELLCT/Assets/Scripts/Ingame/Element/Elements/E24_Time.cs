using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E24_Time : Card
{
    [SerializeField] PhaseController _phaseController = default!;
    [SerializeField] TimeLimitController _timeLimitController = default!;
    [SerializeField] float _addValueInSeconds;
    [SerializeField] float _reduceValueInSeconds;

    public override int Id => 24;

    private void Awake()
    {
        _phaseController.OnTradingPhaseStart.Add(OnTradingPhaseStart);
    }

    private void OnDestroy()
    {
        _phaseController.OnTradingPhaseStart.Remove(OnTradingPhaseStart);
    }

    public override void Buy()
    {
        _timeLimitController.AddTimeLimit(_addValueInSeconds, _handMediator.FindAll(this));
        _moneyPossessedController.DecreaseMoney(_parameter.GetMoney());
    }

    public override void OnPressedU6Button()
    {
        throw new System.NotImplementedException();
    }

    public override void Sell()
    {
        _timeLimitController.ReduceTimeLimit(_reduceValueInSeconds, _handMediator.FindAll(this));
        _moneyPossessedController.IncreaseMoney(_parameter.GetMoney());
    }

    private void OnTradingPhaseStart()
    {
        _timeLimitController.Generate(_handMediator.FindAll(this));
    }
}
