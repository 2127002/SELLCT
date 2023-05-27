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

    private async void Start()
    {
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
        //SE001�̍Đ�
     

        TransitionToInGame();
    }

    public void OnPressedContinue()
    {
        //SE001�̍Đ�


        TransitionToInGame();
    }

    public void OnPressedGameEnd()
    {
        //SE001�̍Đ�

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
