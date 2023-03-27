using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

//�J�[�\���i�N���X�w�A�j�Ɋւ��鏈���ł��B
public class CursorController : MonoBehaviour
{
    [SerializeField] InputActionReference _moveAction = default!;
    [SerializeField] float _cursorSpeed = default!;
    [SerializeField] RectTransform _cursorTransform = default!;
    [SerializeField] PhaseController _phaseController = default!;

    Vector2 _cursorPosition = default!;

    readonly List<RectTransform> _rectTransforms = new();

    bool _isCursorMoving = default!;
    RectTransform _currentSelectedRectTransform = default!;

    private void Awake()
    {
        _phaseController.OnExplorationPhaseStart += Init;
        _phaseController.OnTradingPhaseStart.Add(Init);
    }

    private void OnEnable()
    {
        _moveAction.action.Enable();
        _moveAction.action.performed += OnCursorMove;
        _moveAction.action.canceled += OnCursorMove;
    }

    private void OnDisable()
    {
        _moveAction.action.Disable();
        _moveAction.action.performed -= OnCursorMove;
        _moveAction.action.canceled += OnCursorMove;
    }

    private void OnDestroy()
    {
        _phaseController.OnExplorationPhaseStart -= Init;
        _phaseController.OnTradingPhaseStart.Remove(Init);
    }

    private void Update()
    {
        CursorMoving();

        OnMoving();
    }

    private void CursorMoving()
    {
        _cursorTransform.anchoredPosition += _cursorSpeed * Time.deltaTime * _cursorPosition;
    }

    private void Init()
    {
        _rectTransforms.Clear();

        //IPointerEnter�������A���ݗL���ȃL�����o�X���ɂ���RectTransform���i�[�B������x�̌v�Z�ʂ��K�v
        _rectTransforms.AddRange(FindObjectsOfType<RectTransform>().Where(x => x.GetComponent<IPointerEnterHandler>() != null && x.GetComponentInParent<Canvas>().enabled));
    }

    private void OnMoving()
    {
        //�Î~���͎��s���Ȃ�
        if (!_isCursorMoving) return;

        //�S�{�^���Ɏ��s
        foreach (var rectTrans in _rectTransforms)
        {
            //Canvas��RenderMode���ύX���ꂽ��o�O��܂��B���̐ݒ�ł�WorldSpace��z�肵�Ă��܂�
            bool pointerContains = rectTrans.GetWorldRect(Vector2.one).Contains(_cursorTransform.anchoredPosition);

            //�J�[�\���i�N���X�w�A�j���摜��ɗ�����I������
            if (pointerContains)
            {
                _currentSelectedRectTransform = rectTrans;

                //�I�����ꂽ���Ƃ��I�u�W�F�N�g�ɒʒm����
                ExecuteEvents.Execute(rectTrans.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
            }
        }

        //�I������Ă���I�u�W�F�N�g���Ȃ���Έȍ~�͍s��Ȃ�
        if (_currentSelectedRectTransform == null) return;

        //Canvas��RenderMode���ύX���ꂽ��o�O��܂��B���̐ݒ�ł�WorldSpace��z�肵�Ă��܂�        
        bool currentSelectedContains = _currentSelectedRectTransform.GetWorldRect(Vector2.one).Contains(_cursorTransform.anchoredPosition);

        if (!currentSelectedContains)
        {
            //�I�����O�ꂽ���Ƃ��I�u�W�F�N�g�ɒʒm����
            ExecuteEvents.Execute(_currentSelectedRectTransform.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
            _currentSelectedRectTransform = null;
        }
    }

    private void OnCursorMove(InputAction.CallbackContext context)
    {
        _cursorPosition = context.ReadValue<Vector2>();

        if (context.performed) _isCursorMoving = true;
        if (context.canceled) _isCursorMoving = false;
    }
}
