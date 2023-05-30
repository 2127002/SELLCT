using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using static EndingController;

public class EndingController : MonoBehaviour
{
    [SerializeField] ConversationController _conversationController = default!;

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

        //セーブデータに保存
        DataManager.saveData.hasCollectedEndings[(int)endingScene] = true;
        DataManager.SaveSaveData();

        _endingView.SetText(EndingText(endingScene));

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Title");
        asyncOperation.allowSceneActivation = false;

        await _endingView.StartEndingScene();

        SoundManager.Instance.StopBGM();
        //タイトルに遷移
        asyncOperation.allowSceneActivation = true;
    }

    public async UniTask EndingSceneText(EndingScene endingScene)
    {
        //1フレーム待機
        await UniTask.Yield();
        await _conversationController.OnSceneEnding(endingScene);
    }

    private string EndingText(EndingScene endingScene)
    {
        return endingScene switch
        {
            EndingScene.End1 => "End 4 「壊れた時計」",
            EndingScene.End3 => "End 3 「もう何も見えない」",
            EndingScene.End4 => "End 2 「夜道には気を付けて」",
            EndingScene.End5 => "End 1 「無垢なる死」",
            _ => throw new System.NotImplementedException(),
        };
    }
}
