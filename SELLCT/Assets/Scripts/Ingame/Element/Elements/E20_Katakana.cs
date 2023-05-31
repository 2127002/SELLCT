using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E20_Katakana : Card
{
    [SerializeField] KatakanaHandView _katakanaHandView = default!;
    [SerializeField] PhaseController _phaseController = default!;

    readonly int elementIndex = (int)StringManager.Element.E20;

    public override int Id => 20;

    private void Awake()
    {
        _phaseController.OnTradingPhaseStart.Add(OnPhaseStart);
        _phaseController.OnExplorationPhaseStart += OnPhaseStart;
    }

    private void OnDestroy()
    {
        _phaseController.OnTradingPhaseStart.Remove(OnPhaseStart);
        _phaseController.OnExplorationPhaseStart -= OnPhaseStart;
    }
    private void OnPhaseStart()
    {
        StringManager.hasElements[elementIndex] = _handMediator.ContainsCard(this);
        _katakanaHandView.Set();
    }

    public override void Buy()
    {
        base.Buy();

        StringManager.hasElements[elementIndex] = true;
        _katakanaHandView.Set();
    }

    public override void OnPressedU6Button()
    {
        throw new System.NotImplementedException();
    }

    public override void Sell()
    {
        base.Sell();

        if (_handMediator.ContainsCard(this)) return;
        StringManager.hasElements[elementIndex] = false;
        _katakanaHandView.Set();
    }
}
