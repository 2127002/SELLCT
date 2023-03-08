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
    [SerializeField] TradingPhaseView _view;
    [SerializeField] TradingPhaseCompletionHandler _completionHandler;
    [SerializeField] HandMediator _handMediator;
    [SerializeField] TextBoxView _textBoxView;
    [SerializeField] TraderController _traderController;

    EventSystem _eventSystem;
    GameObject _lastSelectedObject;

    private void Awake()
    {
        _completionHandler.AddListener(OnComplete);

        _eventSystem = EventSystem.current;

        //�v���C���[��D�̔z�z
        _handMediator.InitTakeCard();
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

    private async void OnComplete()
    {
        //�e�L�X�g�̕\��
        _textBoxView.UpdeteText(_traderController.CurrentTrader.EndMessage());
        await _textBoxView.DisplayTextOneByOne();

        //�t�F�[�h�A�E�g
        await _view.StartFadeout();

        //TODO�FBGM1�̃t�F�[�h�A�E�g
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