using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E23_Crosshair : Card
{
    [SerializeField] CursorController _cursorController = default!;

    public override void Buy()
    {
        _controller.DecreaseMoney(_parameter.GetMoney());

        _cursorController.Enable();
    }

    public override void Passive()
    {
        // DoNothing
    }

    public override void Sell()
    {
        _controller.IncreaseMoney(_parameter.GetMoney());

        if (_handMediator.ContainsCard(this)) return;

        _cursorController.Disable();
    }
}
