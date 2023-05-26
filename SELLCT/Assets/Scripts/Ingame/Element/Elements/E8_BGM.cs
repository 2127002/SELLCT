using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E8_BGM : Card
{
    public override int Id => 8;

    public override void Buy()
    {
        base.Buy();

        //TODO:BGMÇÃçƒê∂
    }

    public override void OnPressedU6Button()
    {
        throw new System.NotImplementedException();
    }

    public override void Sell()
    {
        StopBGM();
    }

    private void StopBGM()
    {
        base.Sell();

        if (_handMediator.ContainsCard(this)) return;

        //TODO:BGMÇÃí‚é~
    }
}
