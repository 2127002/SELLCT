using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E19_Hiragana : Card
{
    [SerializeField] HiraganaHandView _hiraganaHandView = default!;
    [SerializeField] PhaseController _phaseController = default!;
    [SerializeField] TradingNextButtonController _tradingNextButtonController = default!;

    readonly int elementIndex = (int)StringManager.Element.E19;

    public override int Id => 19;

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
        _hiraganaHandView.Set();

        if (StringManager.hasElements[elementIndex]) _tradingNextButtonController.OnHiraganaEnabled();
        else _tradingNextButtonController.OnHiraganaDisabled();
    }

    public override void Buy()
    {
        StringManager.hasElements[elementIndex] = true;
        _hiraganaHandView.Set();
        _tradingNextButtonController.OnHiraganaEnabled();
    }

    public override void OnPressedU6Button()
    {
        throw new System.NotImplementedException();
    }

    public override void Sell()
    {
        if (_handMediator.ContainsCard(this)) return;
        StringManager.hasElements[elementIndex] = false;
        _hiraganaHandView.Set();
        _tradingNextButtonController.OnHiraganaDisabled();
    }
}
