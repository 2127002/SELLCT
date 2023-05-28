using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine;

public class E12_Eye : Card
{
    [SerializeField] End_3 end_3;

    public override int Id => 12;

    public override void Buy()
    {
        base.Buy();
    }

    public override void OnPressedU6Button()
    {
        throw new System.NotImplementedException();
    }

    public override void Sell()
    {
        if (GameOverChecker()) return;

        base.Sell();
    }
    private bool GameOverChecker()
    {
        //売った際にEyeが1枚もないならゲームオーバー
        if (_handMediator.ContainsCard(this)) return false;

        end_3.End_3Transition();
        return true;
    }
}
