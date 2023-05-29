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
        //�J�[�\���̖�����
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _backgroundCanvas.enabled = true;
    }

    private async void Start()
    {
        SoundManager.Instance.PlayBGM(SoundSource.BGM02_TITLE);

        InputSystemManager.Instance.ActionDisable();
        EventSystem.current.SetSelectedGameObject(_firstSelectObject);

        //BGM01�̍Đ�

        //���w�i���t�F�[�h�A�E�g
        await _fadeOutView.StartFade();

        //���͎�t�̊J�n
        InputSystemManager.Instance.ActionEnable();
    }

    public void OnPressedNewGame()
    {
        TransitionToInGame();
    }

    public void OnPressedGameEnd()
    {
        //�Q�[�����I������
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    private async void TransitionToInGame()
    {
        InputSystemManager.Instance.ActionDisable();

        //�t�F�[�h�ɍ��킹��BGM���t�F�[�h�A�E�g

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Main");
        asyncOperation.allowSceneActivation = false;

        //���w�i�t�F�[�h�C��
        await _fadeInView.StartFade();

        //�C���Q�[���ɑJ��
        InputSystemManager.Instance.ActionEnable();
        asyncOperation.allowSceneActivation = true;
    }
}
