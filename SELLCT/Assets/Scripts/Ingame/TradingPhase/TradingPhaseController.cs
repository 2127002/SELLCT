using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// �����t�F�[�Y�̐i�s�ɐӔC�����N���X
/// </summary>
public class TradingPhaseController : MonoBehaviour
{
    [SerializeField] TradingPhaseView _view = default!;
    [SerializeField] PhaseController _phaseController = default!;
    [SerializeField] TimeLimitController _timeLimitController = default!;
    [SerializeField] InputActionReference _navigateAction = default!;
    [SerializeField] Selectable _firstSelectable = default!;
    [SerializeField] ConversationController _conversationController = default!;
    [SerializeField] Canvas _canvas = default!;

    GameObject _lastSelectedObject = default!;

    private void Awake()
    {
        _phaseController.OnGameStart.Add(OnGameStart);
        _phaseController.OnTradingPhaseComplete.Add(OnComplete);
        _phaseController.OnTradingPhaseStart.Add(OnPhaseStart);

        //�^�C�����~�b�g�ɂȂ�����t�F�[�Y����������
        _timeLimitController.OnTimeLimit += _phaseController.CompleteTradingPhase;
    }

    private void OnDestroy()
    {
        _phaseController.OnGameStart.Remove(OnGameStart);
        _phaseController.OnTradingPhaseComplete.Remove(OnComplete);
        _phaseController.OnTradingPhaseStart.Remove(OnPhaseStart);

        _timeLimitController.OnTimeLimit -= _phaseController.CompleteTradingPhase;
    }

    private void OnGameStart()
    {
        _canvas.gameObject.SetActive(false);
    }

    //�����t�F�[�Y�J�n������
    private async void OnPhaseStart()
    {
        _canvas.gameObject.SetActive(true);

        _navigateAction.action.performed += OnNavigate;

        _view.OnPhaseStart();

        //1�t���[���ҋ@���Ă���Z���N�g����
        await UniTask.Yield();

        EventSystem.current.SetSelectedGameObject(_firstSelectable.gameObject);
        _firstSelectable.Select();
        _lastSelectedObject = _firstSelectable.gameObject;
    }

    //�����t�F�[�Y�I���������i�ҋ@�j
    private async UniTask OnComplete()
    {
        await _conversationController.OnEnd();

        //�t�F�[�h�A�E�g
        await _view.OnPhaseComplete();

        _canvas.gameObject.SetActive(false);
        _navigateAction.action.performed -= OnNavigate;
        //TODO�FBGM1�̃t�F�[�h�A�E�g
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