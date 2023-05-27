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
    [SerializeField] InputSystemManager _inputSystemManager = default!;

    public async void End_5Transition()
    {
        _timeLimitController.Stop();
        _inputSystemManager.ActionDisable();

        directorOnEnd5.Play();

        var token = this.GetCancellationTokenOnDestroy();

        //END5�̃^�C�����C���̑��b������u1.1�b�v����EndingScene�Ɉȍ~����B���o�I�ɂ킴�ƍs���Ă��镔��
        int duration = (int)((directorOnEnd5.duration - 1.1d) * 1000d);
        await UniTask.Delay(duration, false, PlayerLoopTiming.Update, token);

        _endingController.StartEndingScene(EndingController.EndingScene.End5);
    }
}
