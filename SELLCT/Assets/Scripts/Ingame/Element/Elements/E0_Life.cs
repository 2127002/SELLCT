using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class E0_Life : Card
{
    [Header("End5「心臓とまる」をアタッチしてください")]
    [SerializeField] End_5 end_5;

    public override int Id => 0;

    public override void Buy()
    {
        base.Buy();

        //TODO:SE301の再生
        //TODO:画面全体を脈動させるアニメーション
        //TODO:テキストボックスを更新する
    }

    public override void OnPressedU6Button()
    {
        throw new System.NotImplementedException();
    }

    public override void Sell()
    {
        //ゲームオーバーなら通常演出にはいかない
        if (GameOverChecker()) return;

        base.Sell();
    }

    private bool GameOverChecker()
    {
        //売った際に命が1枚もないならゲームオーバー
        if (_handMediator.ContainsCard(this)) return false;

        end_5.End_5Transition();
        return true;
    }
}