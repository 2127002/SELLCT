using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E17_Number : Card
{
    [SerializeField] NumberHandView _numberHandView = default!;
    [SerializeField] PhaseController _phaseController = default!;

    int elementIndex = (int)StringManager.Element.E17;

    private void Awake()
    {
        _phaseController.OnTradingPhaseStart.Add(OnPhaseStart);
        _phaseController.OnExplorationPhaseStart += OnPhaseStart;
    }

    private void OnPhaseStart()
    {
        StringManager.hasElements[elementIndex] = _handMediator.ContainsCard(this);
        _numberHandView.Set();
    }

    private void OnDestroy()
    {
        _phaseController.OnTradingPhaseStart.Remove(OnPhaseStart);
        _phaseController.OnExplorationPhaseStart -= OnPhaseStart;
    }

    public override void Buy()
    {
        StringManager.hasElements[elementIndex] = true;
        _controller.DecreaseMoney(_parameter.GetMoney());
        _numberHandView.Set();
    }

    public override void Passive()
    {
        throw new System.NotImplementedException();
    }

    public override void Sell()
    {
        _controller.IncreaseMoney(_parameter.GetMoney());

        if (_handMediator.ContainsCard(this)) return;
        StringManager.hasElements[elementIndex] = false;
        _numberHandView.Set();
    }
}
