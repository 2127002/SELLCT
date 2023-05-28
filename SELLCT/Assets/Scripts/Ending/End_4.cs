using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class End_4 : MonoBehaviour
{
    [SerializeField] EndingController _endingController;
    [SerializeField] PlayableDirector directorOnEnd4;

    public async void End_4Transition()
    {
        //TODO:ââèoí«â¡
        directorOnEnd4.Play();

        var token = this.GetCancellationTokenOnDestroy();

        int duration = (int)((directorOnEnd4.duration - 1.1d) * 1000d);
        await UniTask.Delay(duration, false, PlayerLoopTiming.Update, token);
        _endingController.StartEndingScene(EndingController.EndingScene.End4);
    }
}
