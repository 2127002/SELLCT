using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class End_3 : MonoBehaviour
{
    [SerializeField] EndingController _endingController;
    [SerializeField] PlayableDirector directorOnEnd5;
    public async void End_3Transition()
    {
        //TODO:���o�ǉ�
        directorOnEnd5.Play();

        var token = this.GetCancellationTokenOnDestroy();
        
        int duration = (int)((directorOnEnd5.duration - 1.1d) * 1000d);
        await UniTask.Delay(duration, false, PlayerLoopTiming.Update, token);
        
        _endingController.StartEndingScene(EndingController.EndingScene.End3);
    }
}
