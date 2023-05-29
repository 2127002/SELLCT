using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class EndingController : MonoBehaviour
{
    public enum EndingScene
    {
        End1,
        End2,
        End3,
        End4,
        End5,
        End6,
    }

    [SerializeField] EndingView _endingView = default!;

    private void Reset()
    {
        _endingView = GetComponent<EndingView>();
    }

    public async void StartEndingScene(EndingScene endingScene)
    {
        SoundManager.Instance.PlayBGM(SoundSource.BGM03_ENDING);

        //�Z�[�u�f�[�^�ɕۑ�
        DataManager.saveData.hasCollectedEndings[(int)endingScene] = true;
        DataManager.SaveSaveData();

        _endingView.SetText(EndingText(endingScene));

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Title");
        asyncOperation.allowSceneActivation = false;

        await _endingView.StartEndingScene();

        //�^�C�g���ɑJ��
        asyncOperation.allowSceneActivation = true;
    }

    private string EndingText(EndingScene endingScene)
    {
        return endingScene switch
        {
            EndingScene.End1 => "End 1 �u��ꂽ���v�v",
            EndingScene.End2 => "End 2 �uWho is me...?�v",
            EndingScene.End3 => "End 3 �u�������������Ȃ��v",
            EndingScene.End4 => "End 4 �u�铹�ɂ͋C��t���āv",
            EndingScene.End5 => "End 5 �u���C�Ȃ鎀�v",
            EndingScene.End6 => "End 6 �u�����l�ȋP���v",
            _ => throw new System.NotImplementedException(),
        };
    }
}
