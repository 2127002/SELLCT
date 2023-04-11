using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class E7_Move : Card
{
    [SerializeField] Selectable _selectable;
    [SerializeField] Image _u7 = default!;

    public override int Id => 7;

    public override void Buy()
    {
        OnBuy();
    }

    public override void OnPressedU6Button()
    {
        throw new System.NotImplementedException();
    }

    public override void Sell()
    {
        OnSell();
    }

    public void OnBuy()
    {
        _u7.enabled = true;
        _selectable.interactable = true;
    }
    public void OnSell()
    {
        if (_handMediator.ContainsCard(this)) return;
        _u7.enabled = false;
        _selectable.interactable = false;
    }
}
