using System.Collections.Generic;
using UnityEngine;

public class E1_Buy : Card
{
    [SerializeField] CardUIInstance _goodsCardUIInstance = default!;

    public override int Id => 1;

    public override void Buy()
    {
    }

    public override void OnPressedU6Button()
    {
        throw new System.NotImplementedException();
    }

    public override void Sell()
    {
        if (_handMediator.ContainsCard(this)) return;

        DisabledSelectable();
    }    
    
    private void DisabledSelectable()
    {
        foreach (var cardUIHandler in _goodsCardUIInstance.Handlers)
        {
            cardUIHandler.DisableSelectability(InteractableChange.Element);
        }
    }
}
