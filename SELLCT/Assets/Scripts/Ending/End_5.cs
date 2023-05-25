using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class End_5 : MonoBehaviour
{
    [SerializeField] EndingController _endingController;
    [SerializeField] PlayableDirector directorOnEnd5;

    private TimelineAsset _timelineOnEnd5;

    private void Awake()
    {
        _timelineOnEnd5 = directorOnEnd5.playableAsset as TimelineAsset;
    }

    public void End_5Transition()
    {
        //TODO:ââèoí«â¡
        OnPlayTimeLine(directorOnEnd5, _timelineOnEnd5);
        _endingController.StartEndingScene(EndingController.EndingScene.End5);
    }
    private void OnPlayTimeLine(PlayableDirector director, TimelineAsset timeline)
    {
        director.Play(timeline);
    }
}
