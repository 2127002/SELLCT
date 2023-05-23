using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine;

public class E12_Eye : Card
{
    [Header("解像度の売った際演出のタイムラインをいれてください。買う時の演出の処理を行います。")]
    [SerializeField] PlayableDirector directorOnSell;
    [Header("解像度の買った際演出タイムラインをいれてください。売る時の演出の処理を行います。")]
    [SerializeField] PlayableDirector directorOnBuy;
    [SerializeField] End_3 end_3;
    private TimelineAsset _timelineOnBuy;
    private TimelineAsset _timelineOnSell;

    public override int Id => 12;

    private void Awake()
    {
        _timelineOnBuy = directorOnBuy.playableAsset as TimelineAsset;
        _timelineOnSell = directorOnSell.playableAsset as TimelineAsset;
    }

    public override void Buy()
    {
        OnPlayTimeLine(directorOnBuy,_timelineOnBuy);
    }

    public override void OnPressedU6Button()
    {
        throw new System.NotImplementedException();
    }

    public override void Sell()
    {
        OnPlayTimeLine(directorOnSell,_timelineOnSell);
        GameOverChecker();
    }
    private void GameOverChecker()
    {
        //売った際にEyeが1枚もないならゲームオーバー
        if (_handMediator.ContainsCard(this)) return;

        end_3.End_3Transition();
    }
    private void OnPlayTimeLine(PlayableDirector director,TimelineAsset timeline)
    {
        director.Play(timeline);
    }
}
