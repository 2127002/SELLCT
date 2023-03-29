using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E16_Communication : Card
{
    public override void Buy()
    {
        _controller.DecreaseMoney(_parameter.GetMoney());
    }

    public override void Passive()
    {
        throw new System.NotImplementedException();
    }

    public override void Sell()
    {
        _controller.IncreaseMoney(_parameter.GetMoney());
        if (_handMediator.ContainsCard(this)) return;
    }
}
