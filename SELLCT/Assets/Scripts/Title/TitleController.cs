using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.EventSystems;

public class TitleController : MonoBehaviour
{
    [SerializeField] GameObject _firstSelectObject = default!;
    [SerializeField] FadeInView _fadeInView = default!;
    [SerializeField] FadeOutView _fadeOutView = default!;

    [SerializeField] GameObject _ingame = default!;

    private async void Start()
    {
        InputSystemManager.Instance.ActionDisable();
        EventSystem.current.SetSelectedGameObject(_firstSelectObject);

        //BGM01の再生

        //黒背景をフェードアウト
        await _fadeOutView.StartFade();

        //入力受付の開始
        InputSystemManager.Instance.ActionEnable();
    }

    public void OnPressedNewGame()
    {
        //SE001の再生
     

        TransitionToInGame();
    }

    public void OnPressedContinue()
    {
        //SE001の再生


        TransitionToInGame();
    }

    public void OnPressedGameEnd()
    {
        //SE001の再生

        //ゲームを終了する
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    private async void TransitionToInGame()
    {
        InputSystemManager.Instance.ActionDisable();

        //フェードに合わせてBGMをフェードアウト

        //黒背景フェードイン
        await _fadeInView.StartFade();

        //インゲームに遷移
        InputSystemManager.Instance.ActionEnable();
        gameObject.SetActive(false);
        _ingame.SetActive(true);
    }
}
