using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

/// <summary>
/// �����t�F�[�Y�̐i�s�ɐӔC�����N���X
/// </summary>
public class TradingPhaseController : MonoBehaviour
{
    [SerializeField] TimeLimitController _timeLimitController;
    [SerializeField] HandMediator _handMediator;

    ITradingPhaseViewReceiver _view;
    EventSystem _eventSystem;
    GameObject _lastSelectedObject;

    private void Awake()
    {
        _view = GetComponent<ITradingPhaseViewReceiver>();
        _timeLimitController.AddAction(OnTimeLimit);

        _eventSystem = EventSystem.current;

        //�v���C���[��D�̔z�z
        _handMediator.TakeCard();
    }

    private void Start()
    {
        //���ݑI�𒆂�UI�I�u�W�F�N�g��o�^�B
        //�I�𒆃I�u�W�F�N�g�̏�����������Awake�ōs���邽��Start�ɔz�u���Ă��܂��B
        _lastSelectedObject = _eventSystem.currentSelectedGameObject;
    }

    private void OnEnable()
    {
        InputSystemDetector.Instance.AddNavigate(OnNavigate);
    }

    private void OnDisable()
    {
        InputSystemDetector.Instance.RemoveNavigate(OnNavigate);
    }

    private void OnTimeLimit()
    {
        _view.OnTimeLimitReached();
    }

    private void OnNavigate(InputAction.CallbackContext context)
    {
        if (_eventSystem.currentSelectedGameObject != null)
        {
            _lastSelectedObject = _eventSystem.currentSelectedGameObject;
            return;
        }

        //���I�����̂ݎ��s
        _eventSystem.SetSelectedGameObject(_lastSelectedObject);
    }
}