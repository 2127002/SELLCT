using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class End_1 : MonoBehaviour
{
    [SerializeField] EndingController _endingController;
    [Header("END1の演出タイムラインを入れてください。")]
    [SerializeField] PlayableDirector directorOnEnd1;
    
    private TimelineAsset _timelineOnEnd1;
    public void End_1Transition()
    {
        //TODO:演出追加

        _endingController.StartEndingScene(EndingController.EndingScene.End1);
    }
}
