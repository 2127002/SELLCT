using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E8_BGM : Card
{
    public override void Buy()
    {
        //TODO:BGMÇÃçƒê∂

        _controller.DecreaseMoney(_parameter.GetMoney());
    }

    public override void Passive()
    {
        throw new System.NotImplementedException();
    }

    public override void Sell()
    {
        StopBGM();

        _controller.IncreaseMoney(_parameter.GetMoney());
    }

    private void StopBGM()
    {
        if (_handMediator.ContainsCard(this)) return;

        //TODO:BGMÇÃí‚é~
    }
}
