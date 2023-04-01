using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ExplorationPhaseController : MonoBehaviour
{
    [SerializeField] PhaseController _phaseController = default!;
    [SerializeField] Canvas _canvas = default!;
    [SerializeField] Selectable _firstSelectable = default!;
    [SerializeField] InputActionReference _navigateAction = default!;

    GameObject _lastSelectedObject = default!;

    private void Awake()
    {
        _phaseController.OnGameStart.Add(OnGameStart);
        _phaseController.OnExplorationPhaseStart += OnPhaseStart;
        _phaseController.OnExplorationPhaseComplete += OnPhaseComplete;
    }

    private void OnEnable()
    {
        _navigateAction.action.Enable();
        _navigateAction.action.performed += OnNavigate;
    }

    private void OnDisable()
    {
        _navigateAction.action.Disable();
        _navigateAction.action.performed -= OnNavigate;
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

        //������Select�ݒ�
        EventSystem.current.SetSelectedGameObject(_firstSelectable.gameObject);
        _lastSelectedObject = EventSystem.current.currentSelectedGameObject;
        //TODO:BGM2�̍Đ�
    }

    private void OnNavigate(InputAction.CallbackContext context)
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            _lastSelectedObject = EventSystem.current.currentSelectedGameObject;
            return;
        }

        //���I�����̂ݎ��s
        EventSystem.current.SetSelectedGameObject(_lastSelectedObject);
    }
}
