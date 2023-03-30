using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E16_Communication : Card
{
    public override int Id => 16;

    public override void Buy()
    {
        _moneyPossessedCcontroller.DecreaseMoney(_parameter.GetMoney());
    }

    public override void OnPressedU6Button()
    {
        throw new System.NotImplementedException();
    }

    public override void Sell()
    {
        _moneyPossessedCcontroller.IncreaseMoney(_parameter.GetMoney());
        if (_handMediator.ContainsCard(this)) return;
    }
}
