using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E9_SE : Card
{
    public override int Id => 9;

    public override void Buy()
    {
        //TODO:SE�̍Đ�

        _moneyPossessedController.DecreaseMoney(_parameter.GetMoney());
    }

    public override void OnPressedU6Button()
    {
        throw new System.NotImplementedException();
    }

    public override void Sell()
    {
        StopSE();

        _moneyPossessedController.IncreaseMoney(_parameter.GetMoney());
    }

    private void StopSE()
    {
        if (_handMediator.ContainsCard(this)) return;

        //TODO:SE�̒�~
    }
}
