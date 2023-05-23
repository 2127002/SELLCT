using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class E0_Life : Card
{
    [SerializeField] End_5 end_5;
    public override int Id => 0;

    public override void Buy()
    {
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
        GameOverChecker();
    }

    private void GameOverChecker()
    {
        //売った際に命が1枚もないならゲームオーバー
        if (_handMediator.ContainsCard(this)) return;

        Debug.LogWarning(StringManager.ToDisplayString("命がなくなりました！シーン3に遷移する処理は未実装なため続行されます。"));
        end_5.End_5Transition();
    }
}