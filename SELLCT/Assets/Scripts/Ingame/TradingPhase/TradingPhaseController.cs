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
    [SerializeField] PhaseController _phaseController = default!;
    [SerializeField] TimeLimitController _timeLimitController = default!;
    [SerializeField] InputActionReference _navigateAction = default!;
    [SerializeField] Selectable _firstSelectable = default!;
    [SerializeField] ConversationController _conversationController = default!;
    [SerializeField] RugController _rugController = default!;
    [SerializeField] Canvas _canvas = default!;
    [SerializeField] InputActionReference _any = default!;

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
    private void OnPhaseStart()
    {
        _canvas.gameObject.SetActive(true);

        _navigateAction.action.performed += OnNavigate;
    }

    //�����t�F�[�Y�I���������i�ҋ@�j
    private async UniTask OnComplete()
    {
        EventSystem.current.SetSelectedGameObject(null);
        _lastSelectedObject = null;

        await _conversationController.OnEnd();

        await _rugController.PlayEndAnimation();

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

    public async void OnSetTrader()
    {
        var token = this.GetCancellationTokenOnDestroy();

        InputSystemManager.Instance.ActionDisable();

        SetFirstSelectedGameObject();

        //�X�^�[�g���̉�b���s��
        await _conversationController.OnStart();

        EventSystem.current.SetSelectedGameObject(null);
        _lastSelectedObject = null;

        InputSystemManager.Instance.ActionEnable();

        //�L�[���͑ҋ@
        while (!_any.action.WasPressedThisFrame())
        {
            await UniTask.Yield(token);
        }

        //��b�I���ド�O������
        await _rugController.PlayStartAnimation();

        //�������Ԃ̊J�n
        _timeLimitController.Play();

        SetFirstSelectedGameObject();
    }

    private void SetFirstSelectedGameObject()
    {
        EventSystem.current.SetSelectedGameObject(_firstSelectable.gameObject);
        _lastSelectedObject = _firstSelectable.gameObject;
    }
}