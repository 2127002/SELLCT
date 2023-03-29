using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E20_Katakana : Card
{
    [SerializeField] KatakanaHandView _katakanaHandView = default!;
    [SerializeField] PhaseController _phaseController = default!;

    int elementIndex = (int)StringManager.Element.E20;

    private void Awake()
    {
        _phaseController.OnTradingPhaseStart.Add(OnPhaseStart);
        _phaseController.OnExplorationPhaseStart += OnPhaseStart;
    }

    private void OnPhaseStart()
    {
        StringManager.hasElements[elementIndex] = _handMediator.ContainsCard(this);
        _katakanaHandView.Set();
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
        _katakanaHandView.Set();
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
        _katakanaHandView.Set();
    }
}
