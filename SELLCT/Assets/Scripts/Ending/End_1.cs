using Cysharp.Threading.Tasks;
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
    [SerializeField] TimeLimitController _timeLimitController = default!;
    [SerializeField] InputSystemManager _inputSystemManager = default!;

    public async void End_1Transition()
    {
        _timeLimitController.Stop();
        _inputSystemManager.ActionDisable();

        directorOnEnd1.Play();
        var token = this.GetCancellationTokenOnDestroy();

        int duration = (int)((directorOnEnd1.duration - 1.0d) * 1000d);
        await UniTask.Delay(duration, false, PlayerLoopTiming.Update, token);

        _endingController.StartEndingScene(EndingController.EndingScene.End1);
    }
}
