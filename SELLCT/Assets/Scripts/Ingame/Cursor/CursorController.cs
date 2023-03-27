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

        //IPointerEnterを持ち、現在有効なキャンバス内にあるRectTransformを格納。ある程度の計算量が必要
        _rectTransforms.AddRange(FindObjectsOfType<RectTransform>().Where(x => x.GetComponent<IPointerEnterHandler>() != null && x.GetComponentInParent<Canvas>().enabled));
    }

    private void OnMoving()
    {
        //静止中は実行しない
        if (!_isCursorMoving) return;

        //全ボタンに実行
        foreach (var rectTrans in _rectTransforms)
        {
            //CanvasのRenderModeが変更されたらバグります。この設定ではWorldSpaceを想定しています
            bool pointerContains = rectTrans.GetWorldRect(Vector2.one).Contains(_cursorTransform.anchoredPosition);

            //カーソル（クロスヘア）が画像上に来たら選択する
            if (pointerContains)
            {
                _currentSelectedRectTransform = rectTrans;

                //選択されたことをオブジェクトに通知する
                ExecuteEvents.Execute(rectTrans.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
            }
        }

        //選択されているオブジェクトがなければ以降は行わない
        if (_currentSelectedRectTransform == null) return;

        //CanvasのRenderModeが変更されたらバグります。この設定ではWorldSpaceを想定しています        
        bool currentSelectedContains = _currentSelectedRectTransform.GetWorldRect(Vector2.one).Contains(_cursorTransform.anchoredPosition);

        if (!currentSelectedContains)
        {
            //選択が外れたことをオブジェクトに通知する
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
