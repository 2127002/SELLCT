using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    [SerializeField] GameObject _firstSelectObject = default!;
    [SerializeField] FadeInView _fadeInView = default!;
    [SerializeField] FadeOutView _fadeOutView = default!;

    [SerializeField] Canvas _backgroundCanvas = default!;

    private void Awake()
    {
        //カーソルの無効化
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _backgroundCanvas.enabled = true;
    }

    private async void Start()
    {
        SoundManager.Instance.PlayBGM(SoundSource.BGM02_TITLE);

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
        TransitionToInGame();
    }

    public void OnPressedGameEnd()
    {
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

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Main");
        asyncOperation.allowSceneActivation = false;

        //黒背景フェードイン
        await _fadeInView.StartFade();

        //インゲームに遷移
        InputSystemManager.Instance.ActionEnable();
        asyncOperation.allowSceneActivation = true;
    }
}
