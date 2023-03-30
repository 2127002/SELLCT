using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E8_BGM : Card
{
    public override int Id => 8;

    public override void Buy()
    {
        //TODO:BGMÇÃçƒê∂

        _moneyPossessedCcontroller.DecreaseMoney(_parameter.GetMoney());
    }

    public override void OnPressedU6Button()
    {
        throw new System.NotImplementedException();
    }

    public override void Sell()
    {
        StopBGM();

        _moneyPossessedCcontroller.IncreaseMoney(_parameter.GetMoney());
    }

    private void StopBGM()
    {
        if (_handMediator.ContainsCard(this)) return;

        //TODO:BGMÇÃí‚é~
    }
}
