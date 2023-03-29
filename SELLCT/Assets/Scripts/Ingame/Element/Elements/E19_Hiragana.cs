using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E19_Hiragana : Card
{
    [SerializeField] HiraganaHandView _hiraganaHandView = default!;
    [SerializeField] PhaseController _phaseController = default!;

    int elementIndex = (int)StringManager.Element.E19;

    private void Awake()
    {
        _phaseController.OnTradingPhaseStart.Add(OnPhaseStart);
        _phaseController.OnExplorationPhaseStart += OnPhaseStart;
    }

    private void OnPhaseStart()
    {
        StringManager.hasElements[elementIndex] = _handMediator.ContainsCard(this);
        _hiraganaHandView.Set();
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
        _hiraganaHandView.Set();
    }

    public override void Passive()
    {
        // DoNothing
    }

    public override void Sell()
    {
        _controller.IncreaseMoney(_parameter.GetMoney());

        if (_handMediator.ContainsCard(this)) return;
        StringManager.hasElements[elementIndex] = false;
        _hiraganaHandView.Set();
    }
}
