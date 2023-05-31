using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using static EndingController;

public class End_1 : MonoBehaviour
{
    [SerializeField] EndingController _endingController;
    [Header("END1の演出タイムラインを入れてください。")]
    [SerializeField] PlayableDirector directorOnEnd1;
    [SerializeField] TimeLimitController _timeLimitController = default!;

    public async void End_1Transition()
    {
        var token = this.GetCancellationTokenOnDestroy();

        _timeLimitController.Stop();
        InputSystemManager.Instance.ActionDisable();

        _endingController.EndingSceneText(EndingController.EndingScene.End1).Forget();
        
        directorOnEnd1.Play();

        int duration = (int)((directorOnEnd1.duration - 1.0d) * 1000d);

        await UniTask.Delay(duration, false, PlayerLoopTiming.Update, token);

        DataManager.saveData.sceneNum = Mathf.Max(DataManager.saveData.sceneNum, 4);
        DataManager.SaveSaveData();

        _endingController.StartEndingScene(EndingController.EndingScene.End1);
    }
}
