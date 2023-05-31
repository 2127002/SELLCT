using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E21_Alphabet : Card
{
    [SerializeField] AlphabetHandView _alphabetHandView = default!;
    [SerializeField] PhaseController _phaseController = default!;
    [SerializeField] MoneyPossessedController _moneyPossessedController = default!;

    readonly int elementIndex = (int)StringManager.Element.E21;

    public override int Id => 21;

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
        _alphabetHandView.Set();
    }

    public override void Buy()
    {
        base.Buy();

        StringManager.hasElements[elementIndex] = true;
        _moneyPossessedController.EnableAlphabet();
        _alphabetHandView.Set();
    }

    public override void OnPressedU6Button()
    {
        throw new System.NotImplementedException();
    }

    public override void Sell()
    {
        base.Sell();

        if (_handMediator.ContainsCard(this)) return;
        _moneyPossessedController.DisableAlphabet();
        StringManager.hasElements[elementIndex] = false;
        _alphabetHandView.Set();
    }
}
