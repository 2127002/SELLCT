using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E37_Lack : Card
{
    public override void Buy()
    {
        _controller.DecreaseMoney(_parameter.GetMoney());
    }

    public override void Passive()
    {
        //Do Nothing...
        throw new System.NotImplementedException();
    }

    public override void Sell()
    {
        _controller.IncreaseMoney(_parameter.GetMoney());
    }

}
