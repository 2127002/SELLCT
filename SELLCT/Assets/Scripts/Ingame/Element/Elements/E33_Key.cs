using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class E33_Key : Card
{
    public override int Id => 33;

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
