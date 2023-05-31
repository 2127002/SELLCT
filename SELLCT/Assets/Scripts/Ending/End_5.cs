using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class End_5 : MonoBehaviour
{
    [SerializeField] EndingController _endingController = default!;
    [SerializeField] PlayableDirector directorOnEnd5 = default!;
    [SerializeField] TimeLimitController _timeLimitController = default!;

    public async void End_5Transition()
    {
        _timeLimitController.Stop();
        InputSystemManager.Instance.ActionDisable();
        _endingController.EndingSceneText(EndingController.EndingScene.End5).Forget();

        directorOnEnd5.Play();

        var token = this.GetCancellationTokenOnDestroy();

        //END5のタイムラインの総秒数から「1.1秒」早くEndingSceneに以降する。演出的にわざと行っている部分
        int duration = (int)((directorOnEnd5.duration - 1.1d) * 1000d);
        await UniTask.Delay(duration, false, PlayerLoopTiming.Update, token);
        DataManager.saveData.sceneNum = Mathf.Max(DataManager.saveData.sceneNum, 1);
        DataManager.SaveSaveData();

        _endingController.StartEndingScene(EndingController.EndingScene.End5);
    }
}
