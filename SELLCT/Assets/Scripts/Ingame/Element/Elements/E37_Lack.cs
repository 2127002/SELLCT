using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E37_Lack : Card
{
    public override int Id => 37;

    public override void Buy()
    {
        _moneyPossessedController.DecreaseMoney(_parameter.GetMoney());
    }

    public override void OnPressedU6Button()
    {
        //Do Nothing...
        throw new System.NotImplementedException();
    }

    public override void Sell()
    {
        _moneyPossessedController.IncreaseMoney(_parameter.GetMoney());
    }

}
