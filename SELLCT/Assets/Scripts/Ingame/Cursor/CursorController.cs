using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

//カーソル（クロスヘア）に関する処理です。
public class CursorController : MonoBehaviour
{
    [SerializeField] CursorView _cursorView = default!;

    [SerializeField] InputActionReference _moveAction = default!;
    [SerializeField] InputActionReference _clickAction = default!;
    [SerializeField] float _cursorSpeed = default!;
    [SerializeField] RectTransform _cursorTransform = default!;
    [SerializeField] PhaseController _phaseController = default!;

    Vector2 _moveAxis = default!;
    Vector2 _cursorPos = default!;
    Vector2 _prebPos = default!;

    readonly List<RectTransform> _rectTransforms = new();

    RectTransform _currentSelectedRectTransform = default!;

    const float MAXWIDTH = 1920f;
    const float MAXHEIGHT = 1080f;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _phaseController.OnExplorationPhaseStart += Init;
        _phaseController.OnTradingPhaseStart.Add(Init);
    }

    private void OnEnable()
    {
        _moveAction.action.Enable();
        _moveAction.action.performed += OnCursorMove;
        _moveAction.action.canceled += OnCursorMove;
        _clickAction.action.Enable();
        _clickAction.action.performed += OnClick;
    }

    private void OnDisable()
    {
        _moveAction.action.Disable();
        _moveAction.action.performed -= OnCursorMove;
        _moveAction.action.canceled += OnCursorMove;
        _clickAction.action.Disable();
        _clickAction.action.performed -= OnClick;
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
        _cursorTransform.anchoredPosition += _cursorSpeed * Time.deltaTime * _moveAxis;

        //カーソルが画面外に出ないように調整
        float posX = Mathf.Clamp(_cursorTransform.anchoredPosition.x, -MAXWIDTH / 2 + _cursorView.CursorSizeDelta.x / 2, MAXWIDTH / 2 - _cursorView.CursorSizeDelta.x / 2);
        float posY = Mathf.Clamp(_cursorTransform.anchoredPosition.y, -MAXHEIGHT / 2 + _cursorView.CursorSizeDelta.y / 2, MAXHEIGHT / 2 - _cursorView.CursorSizeDelta.y / 2); ;
        _cursorPos.Set(posX, posY);

        _cursorTransform.anchoredPosition = _cursorPos;
    }

    private void Init()
    {
        _rectTransforms.Clear();

        //IPointerEnterを持ち、現在有効なキャンバス内にあるRectTransformを格納。ある程度の計算量が必要
        _rectTransforms.AddRange(FindObjectsByType<RectTransform>(FindObjectsSortMode.None).Where(x => x.GetComponent<IPointerEnterHandler>() != null && x.GetComponentInParent<Canvas>().enabled));
    }

    private void OnMoving()
    {
        //静止中は実行しない
        if (_prebPos == _cursorPos) return;
        _prebPos = _cursorPos;

        //クロスヘアの下にある選択できるRectTransformを取得
        RectTransform selected = GetRectTransformAtCrosshair();

        //何もなかったら抜ける
        if (selected == null)
        {
            //選択されているオブジェクトがなければ以降は行わない
            if (_currentSelectedRectTransform == null)
            {
                EventSystem.current.SetSelectedGameObject(null);
                return;
            }

            //選択が外れたことをオブジェクトに通知する
            ExecuteEvents.Execute(_currentSelectedRectTransform.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
            _currentSelectedRectTransform = null;
            return;
        }

        //前回の選択と同じだったら以降は実行しない
        if (selected.gameObject == EventSystem.current.currentSelectedGameObject) return;

        //現在の選択画像を変更
        _currentSelectedRectTransform = selected;

        //選択されたことをオブジェクトに通知する
        ExecuteEvents.Execute(_currentSelectedRectTransform.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
    }

    private RectTransform GetRectTransformAtCrosshair()
    {
        //全ボタンに実行
        foreach (RectTransform rectTransform in _rectTransforms)
        {
            //CanvasのRenderModeが変更されたらバグります。この設定ではWorldSpaceを想定しています
            bool pointerContains = rectTransform.GetWorldRect(Vector2.one).Contains(_cursorTransform.anchoredPosition);

            //カーソル（クロスヘア）が選択画像上でなかったら次の画像へ
            if (!pointerContains) continue;

            //発見したら抜ける
            return rectTransform;
        }

        return null;
    }

    private void OnCursorMove(InputAction.CallbackContext context)
    {
        _moveAxis = context.ReadValue<Vector2>();
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        if (_currentSelectedRectTransform == null) return;

        ExecuteEvents.Execute(_currentSelectedRectTransform.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
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
