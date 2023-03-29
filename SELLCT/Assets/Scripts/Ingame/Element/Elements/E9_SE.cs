using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E9_SE : Card
{
    public override void Buy()
    {
        //TODO:SEÇÃçƒê∂

        _controller.DecreaseMoney(_parameter.GetMoney());
    }

    public override void Passive()
    {
        // DoNothing
    }

    public override void Sell()
    {
        StopSE();

        _controller.IncreaseMoney(_parameter.GetMoney());
    }

    private void StopSE()
    {
        if (_handMediator.ContainsCard(this)) return;

        //TODO:SEÇÃí‚é~
    }
}
