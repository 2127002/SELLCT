using Cysharp.Threading.Tasks;
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
    [SerializeField] TradingPhaseView _view = default!;
    [SerializeField] TextBoxController _textBoxController = default!;
    [SerializeField] TraderController _traderController = default!;
    [SerializeField] PhaseController _phaseController = default!;
    [SerializeField] TimeLimitController _timeLimitController = default!;
    [SerializeField] InputSystemDetector _inputSystemDetector = default!;
    [SerializeField] CardUIHandler _firstSelectable = default!;
    [SerializeField] PlayerMonologue _playerMonologue = default!;
    [SerializeField] Canvas _canvas = default!;

    GameObject _lastSelectedObject = default!;

    private void Awake()
    {
        _phaseController.OnGameStart.Add(OnGameStart);
        _phaseController.OnTradingPhaseComplete.Add(OnComplete);
        _phaseController.OnTradingPhaseStart.Add(OnPhaseStart);

        //�^�C�����~�b�g�ɂȂ�����t�F�[�Y����������
        _timeLimitController.OnTimeLimit += _phaseController.CompleteTradingPhase;

        _inputSystemDetector.OnNavigateAction += OnNavigate;
    }

    private void Start()
    {
        //���ݑI�𒆂�UI�I�u�W�F�N�g��o�^�B
        //�I�𒆃I�u�W�F�N�g�̏�����������Awake�ōs���邽��Start�ɔz�u���Ă��܂��B
        _lastSelectedObject = EventSystem.current.currentSelectedGameObject;
    }

    private void OnDestroy()
    {
        _phaseController.OnGameStart.Remove(OnGameStart);
        _phaseController.OnTradingPhaseComplete.Remove(OnComplete);
        _phaseController.OnTradingPhaseStart.Remove(OnPhaseStart);

        _timeLimitController.OnTimeLimit -= _phaseController.CompleteTradingPhase;
        _inputSystemDetector.OnNavigateAction -= OnNavigate;
    }

    private void OnGameStart()
    {
        _canvas.gameObject.SetActive(false);
    }

    //�����t�F�[�Y�J�n������
    private void OnPhaseStart()
    {
        _view.OnPhaseStart();
        EventSystem.current.SetSelectedGameObject(_firstSelectable.gameObject);
        _canvas.gameObject.SetActive(true);
    }

    //�����t�F�[�Y�I���������i�ҋ@�j
    private async UniTask OnComplete()
    {
        //�v���C���[�̓Ɣ��ɒu�������邩���肷��
        string speaker = _playerMonologue.SwitchToPlayerMonologue ?
            _playerMonologue.Speaker : _traderController.CurrentTrader.Name;

        string endMessage = _playerMonologue.SwitchToPlayerMonologue ?
            _playerMonologue.EndMessage() : _traderController.CurrentTrader.EndMessage();

        //�e�L�X�g�̕\��
        await _textBoxController.UpdateText(speaker, endMessage);

        //�t�F�[�h�A�E�g
        await _view.OnPhaseComplete();

        _canvas.gameObject.SetActive(false);
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