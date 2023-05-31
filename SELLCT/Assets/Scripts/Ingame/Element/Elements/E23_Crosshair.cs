using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E23_Crosshair : Card
{
    [SerializeField] CursorController _cursorController = default!;

    public override int Id => 23;

    public override void Buy()
    {
        base.Buy();

        _cursorController.Enable();
    }

    public override void OnPressedU6Button()
    {
        throw new System.NotImplementedException();
    }

    public override void Sell()
    {
        base.Sell();

        if (_handMediator.ContainsCard(this)) return;

        _cursorController.Disable();
    }
}
