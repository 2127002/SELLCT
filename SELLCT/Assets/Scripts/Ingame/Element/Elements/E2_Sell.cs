using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class E2_Sell : Card
{
    [SerializeField] CardUIInstance _handCardUIInstance = default!;

    public override int Id => 2;

    public override void Buy()
    {
        base.Buy();

        EnabledSelectable();
    }

    public override void OnPressedU6Button()
    {
        throw new System.NotImplementedException();
    }

    public override void Sell()
    {
        base.Sell();

        if (_handMediator.ContainsCard(this)) return;

        DisabledSelectable();
    }

    private void EnabledSelectable()
    {
        foreach (var cardUIHandler in _handCardUIInstance.Handlers)
        {
            cardUIHandler.EnableBuying(EnableBuyingChange.Element);
        }
    }

    private void DisabledSelectable()
    {
        foreach (var cardUIHandler in _handCardUIInstance.Handlers)
        {
            cardUIHandler.DisableBuying(EnableBuyingChange.Element);
        }
    }
}
