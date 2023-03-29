using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class E0_Life : Card
{
    public override void Buy()
    {
        //TODO:SE301の再生
        //TODO:画面全体を脈動させるアニメーション
        //TODO:テキストボックスを更新する

        _controller.DecreaseMoney(_parameter.GetMoney());
    }

    public override void Passive()
    {
        // DoNothing
    }

    public override void Sell()
    {
        _controller.IncreaseMoney(_parameter.GetMoney());

        GameOverChecker();
    }

    private void GameOverChecker()
    {
        //売った際に命が1枚もないならゲームオーバー
        if (_handMediator.ContainsCard(this)) return;

        Debug.LogWarning(StringManager.ToDisplayString("命がなくなりました！シーン3に遷移する処理は未実装なため続行されます。"));
        //TODO:シーン3に遷移
    }
}