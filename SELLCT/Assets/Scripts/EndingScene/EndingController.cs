using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
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
        _endingView.SetText(EndingText(endingScene));
        await _endingView.StartEndingScene();

        //タイトルに遷移
        Debug.LogWarning("タイトルに遷移する処理が未実装です。");
    }

    private string EndingText(EndingScene endingScene)
    {
        return endingScene switch
        {
            EndingScene.End1 => "End 1 「壊れた時計」",
            EndingScene.End2 => "End 2 「Who is me...?」",
            EndingScene.End3 => "End 3 「もう何も見えない」",
            EndingScene.End4 => "End 4 「夜道には気を付けて」",
            EndingScene.End5 => "End 5 「無垢なる死」",
            EndingScene.End6 => "End 6 「無価値な輝き」",
            _ => throw new System.NotImplementedException(),
        };
    }
}
