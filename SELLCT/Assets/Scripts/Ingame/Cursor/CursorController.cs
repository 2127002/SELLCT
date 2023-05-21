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
    [SerializeField] CursorView _cursorView = default!;

    [SerializeField] InputActionReference _moveAction = default!;
    [SerializeField] InputActionReference _pointerUpAction = default!;
    [SerializeField] InputActionReference _pointerDownAction = default!;
    [SerializeField] float _cursorSpeed = default!;
    [SerializeField] RectTransform _cursorTransform = default!;
    [SerializeField] PhaseController _phaseController = default!;

    Vector2 _moveAxis = default!;
    Vector2 _cursorPos = default!;
    Vector2 _prebPos = default!;

    readonly List<RectTransform> _rectTransforms = new();

    RectTransform _currentSelectedRectTransform = default!;

    DragAndDropController _dragAndDropController = default!;

    const float MAXWIDTH = 1920f;
    const float MAXHEIGHT = 1080f;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _dragAndDropController = new(_cursorTransform);

        _phaseController.OnExplorationPhaseStart += OnExplorationPhaseStart;
        _phaseController.OnTradingPhaseStart.Add(OnTradingPhaseStart);
    }

    private void OnEnable()
    {
        _moveAction.action.Enable();
        _moveAction.action.performed += OnCursorMove;
        _moveAction.action.canceled += OnCursorMove;
        _pointerUpAction.action.Enable();
        _pointerUpAction.action.performed += OnPointerUp;
        _pointerDownAction.action.performed += OnPointerDown;
    }

    private void OnDisable()
    {
        _moveAction.action.Disable();
        _moveAction.action.performed -= OnCursorMove;
        _moveAction.action.canceled += OnCursorMove;
        _pointerUpAction.action.Disable();
        _pointerUpAction.action.performed -= OnPointerUp;
        _pointerDownAction.action.performed -= OnPointerDown;
    }

    private void OnDestroy()
    {
        _phaseController.OnExplorationPhaseStart -= OnExplorationPhaseStart;
        _phaseController.OnTradingPhaseStart.Remove(OnExplorationPhaseStart);
    }

    private void Update()
    {
        CursorMoving();

        OnMoving();
    }

    private void CursorMoving()
    {
        _cursorTransform.anchoredPosition += _cursorSpeed * Time.deltaTime * _moveAxis;

        //�J�[�\������ʊO�ɏo�Ȃ��悤�ɒ���
        const float RANGE_X = MAXWIDTH / 2f - _cursorView.CursorSizeDelta.x / 2f;
        float posX = Mathf.Clamp(_cursorTransform.anchoredPosition.x, -RANGE_X, RANGE_X);
        const float RANGE_Y = MAXHEIGHT / 2f - _cursorView.CursorSizeDelta.y / 2f;
        float posY = Mathf.Clamp(_cursorTransform.anchoredPosition.y, -RANGE_Y, RANGE_Y);
        _cursorPos.Set(posX, posY);

        _cursorTransform.anchoredPosition = _cursorPos;
    }

    private void OnExplorationPhaseStart()
    {
        Enable();

        _rectTransforms.Clear();

        //IPointerEnter�������A���ݗL���ȃL�����o�X���ɂ���RectTransform���i�[�B������x�̌v�Z�ʂ��K�v
        _rectTransforms.AddRange(FindObjectsByType<RectTransform>(FindObjectsSortMode.None).Where(x => x.GetComponent<IPointerEnterHandler>() != null && x.GetComponentInParent<Canvas>().enabled));
    }

    private void OnTradingPhaseStart()
    {
        Disable();
    }

    private void OnMoving()
    {
        //�Î~���͎��s���Ȃ�
        if (_prebPos == _cursorPos) return;
        _prebPos = _cursorPos;

        //D&D���Ȃ炱���Ŕ�����
        if (_dragAndDropController.IsDragging)
        {
            //�I������Ă���I�u�W�F�N�g���Ȃ���Έȍ~�͍s��Ȃ�
            if (_currentSelectedRectTransform == null)
            {
                EventSystem.current.SetSelectedGameObject(null);
                return;
            }

            //�I�����O�ꂽ���Ƃ��I�u�W�F�N�g�ɒʒm����
            ExecuteEvents.Execute(_currentSelectedRectTransform.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
            _currentSelectedRectTransform = null;
            return;
        }

        _dragAndDropController.OnCursorMoving();

        //�N���X�w�A�̉��ɂ���I���ł���RectTransform���擾
        RectTransform selected = GetRectTransformAtCrosshair();

        //�����Ȃ������甲����
        if (selected == null)
        {
            //�I������Ă���I�u�W�F�N�g���Ȃ���Έȍ~�͍s��Ȃ�
            if (_currentSelectedRectTransform == null)
            {
                EventSystem.current.SetSelectedGameObject(null);
                return;
            }

            //�I�����O�ꂽ���Ƃ��I�u�W�F�N�g�ɒʒm����
            ExecuteEvents.Execute(_currentSelectedRectTransform.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
            _currentSelectedRectTransform = null;
            return;
        }

        //�O��̑I���Ɠ�����������ȍ~�͎��s���Ȃ�
        if (selected.gameObject == EventSystem.current.currentSelectedGameObject) return;

        //���݂̑I���摜��ύX
        _currentSelectedRectTransform = selected;

        //�I�����ꂽ���Ƃ��I�u�W�F�N�g�ɒʒm����
        ExecuteEvents.Execute(_currentSelectedRectTransform.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
    }

    private RectTransform GetRectTransformAtCrosshair()
    {
        //�S�{�^���Ɏ��s
        foreach (RectTransform rectTransform in _rectTransforms)
        {
            //Canvas��RenderMode���ύX���ꂽ��o�O��܂��B���̐ݒ�ł�WorldSpace��z�肵�Ă��܂�
            bool pointerContains = rectTransform.GetWorldRect(Vector2.one).Contains(_cursorTransform.anchoredPosition);

            //�J�[�\���i�N���X�w�A�j���I���摜��łȂ������玟�̉摜��
            if (!pointerContains) continue;

            //���������甲����
            return rectTransform;
        }

        return null;
    }

    private void OnCursorMove(InputAction.CallbackContext context)
    {
        _moveAxis = context.ReadValue<Vector2>();
    }

    private void OnPointerUp(InputAction.CallbackContext context)
    {
        //D&D���Ȃ�h���b�v���Ĕ�����
        if (_dragAndDropController.IsDragging)
        {
            _dragAndDropController.Drop();
            return;
        }

        _dragAndDropController.OnPointerUp();

        if (_currentSelectedRectTransform == null) return;

        ExecuteEvents.Execute(_currentSelectedRectTransform.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerUpHandler);
    }

    private void OnPointerDown(InputAction.CallbackContext obj)
    {
        if (_currentSelectedRectTransform == null) return;

        _dragAndDropController.OnPointerDown(_currentSelectedRectTransform);
        ExecuteEvents.Execute(_currentSelectedRectTransform.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);
    }

    public void Enable()
    {
        enabled = true;
        _cursorView.Enable();
    }

    public void Disable()
    {
        enabled = false;
        _cursorView.Disable();
    }

    public void AddRectTransform(RectTransform rectTransform)
    {
        _rectTransforms.Add(rectTransform);
    }

    public void RemoveRectTransform(RectTransform rectTransform)
    {
        _rectTransforms.Remove(rectTransform);
    }
}
