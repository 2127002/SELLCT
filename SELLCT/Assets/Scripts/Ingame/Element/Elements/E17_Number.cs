using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E17_Number : Card
{
    [SerializeField] NumberHandView _numberHandView = default!;
    [SerializeField] PhaseController _phaseController = default!;
    [SerializeField] DeckUIController _deckUIController = default!;
    [SerializeField] MoneyPossessedController _moneyPossessedController = default!;

    readonly int elementIndex = (int)StringManager.Element.E17;

    public override int Id => 17;

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
        _numberHandView.Set();

        if (StringManager.hasElements[elementIndex]) _deckUIController.EnableNumber();
        else _deckUIController.DisableNumber();
    }

    public override void Buy()
    {
        StringManager.hasElements[elementIndex] = true;
        _moneyPossessedController.EnableNumber();
        _numberHandView.Set();
        _deckUIController.EnableNumber();
    }

    public override void OnPressedU6Button()
    {
        throw new System.NotImplementedException();
    }

    public override void Sell()
    {
        if (_handMediator.ContainsCard(this)) return;
        _moneyPossessedController.DisableNumber();
        StringManager.hasElements[elementIndex] = false;
        _numberHandView.Set();
        _deckUIController.DisableNumber();
    }
}
