using System.Collections.Generic;
using UnityEngine;

public class E1_Buy : Card
{
    [SerializeField] CardUIInstance _goodsCardUIInstance = default!;
    public override int Id => 1;
    public override void Buy()
    {
        base.Buy();
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
    private void DisabledSelectable()
    {
        foreach (var cardUIHandler in _goodsCardUIInstance.Handlers)
        {
            cardUIHandler.DisableBuying(EnableBuyingChange.Element);
        }
    }
}
