using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E18_Kanji : Card
{
    [SerializeField] KanjiHandView _kanjiHandView = default!;
    [SerializeField] PhaseController _phaseController = default!;
    [SerializeField] TradingNextButtonController _tradingNextButtonController = default!;

    readonly int elementIndex = (int)StringManager.Element.E18;

    public override int Id => 18;

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
        _kanjiHandView.Set();

        if (StringManager.hasElements[elementIndex]) _tradingNextButtonController.OnKanjiEnabled();
        else _tradingNextButtonController.OnKanjiDisabled();
    }

    public override void Buy()
    {
        StringManager.hasElements[elementIndex] = true;
        _moneyPossessedController.DecreaseMoney(_parameter.GetMoney());
        _kanjiHandView.Set();
        _tradingNextButtonController.OnKanjiEnabled();
    }

    public override void OnPressedU6Button()
    {
        throw new System.NotImplementedException();
    }

    public override void Sell()
    {
        _moneyPossessedController.IncreaseMoney(_parameter.GetMoney());

        if (_handMediator.ContainsCard(this)) return;
        StringManager.hasElements[elementIndex] = false;
        _kanjiHandView.Set();
        _tradingNextButtonController.OnKanjiDisabled();
    }
}
