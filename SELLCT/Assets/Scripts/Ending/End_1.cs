using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class End_1 : MonoBehaviour
{
    [SerializeField] EndingController _endingController;
    [Header("END1�̉��o�^�C�����C�������Ă��������B")]
    [SerializeField] PlayableDirector directorOnEnd1;
    
    private TimelineAsset _timelineOnEnd1;
    public void End_1Transition()
    {
        //TODO:���o�ǉ�

        _endingController.StartEndingScene(EndingController.EndingScene.End1);
    }
}
