using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ExplorationPhaseController : MonoBehaviour
{
    [SerializeField] PhaseController _phaseController;
    [SerializeField] Canvas _canvas;

    private void Awake()
    {
        _phaseController.OnGameStart.Add(OnGameStart);
        _phaseController.OnExplorationPhaseStart += OnPhaseStart;
        _phaseController.OnExplorationPhaseComplete += OnPhaseComplete;
    }

    private void OnGameStart()
    {
        //�L�����o�X��enabled��ύX���邾���ł�Selectable���������Ă��܂�����GameObject��Active��ύX���܂��B
        _canvas.gameObject.SetActive(false);
    }

    private void OnPhaseComplete()
    {
        //�L�����o�X��enabled��ύX���邾���ł�Selectable���������Ă��܂�����GameObject��Active��ύX���܂��B
        _canvas.gameObject.SetActive(false);
    }

    private void OnPhaseStart()
    {
        //�L�����o�X��enabled��ύX���邾���ł�Selectable���������Ă��܂�����GameObject��Active��ύX���܂��B
        _canvas.gameObject.SetActive(true);

        //TODO:BGM2�̍Đ�
    }
}
