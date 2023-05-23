using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine;

public class E12_Eye : Card
{
    [Header("�𑜓x�̔������ۉ��o�̃^�C�����C��������Ă��������B�������̉��o�̏������s���܂��B")]
    [SerializeField] PlayableDirector directorOnSell;
    [Header("�𑜓x�̔������ۉ��o�^�C�����C��������Ă��������B���鎞�̉��o�̏������s���܂��B")]
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
        //�������ۂ�Eye��1�����Ȃ��Ȃ�Q�[���I�[�o�[
        if (_handMediator.ContainsCard(this)) return;

        end_3.End_3Transition();
    }
    private void OnPlayTimeLine(PlayableDirector director,TimelineAsset timeline)
    {
        director.Play(timeline);
    }
}
