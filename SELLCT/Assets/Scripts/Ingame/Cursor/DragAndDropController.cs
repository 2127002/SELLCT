using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragAndDropController
{
    //カーソルのTransform
    RectTransform _cursorRectTransform = default!;

    //D&D前の情報
    Vector3 _prebPosition = default!;
    Transform _prebParent = default!;

    //ドラッグ中のTransform
    RectTransform _rectTransformToMove = default!;

    bool _isDragging = false;

    public DragAndDropController(RectTransform cursorRectTransform)
    {
        _cursorRectTransform = cursorRectTransform;
    }

    public void OnPointerDown(RectTransform rectTransformToMove)
    {
        if (rectTransformToMove == null) throw new System.ArgumentNullException("動かしたいTransformがnullです");

        //D&D前の情報を保存
        _prebPosition = rectTransformToMove.localPosition;
        _prebParent = rectTransformToMove.parent;
        _rectTransformToMove = rectTransformToMove;
    }

    public void OnCursorMoving()
    {
        if (_isDragging) return;
        if (_rectTransformToMove == null) return;

        //カーソルを親にしてドラッグ状態にする
        _rectTransformToMove.SetParent(_cursorRectTransform);
        _rectTransformToMove.localPosition = Vector3.zero;
        _isDragging = true;
    }

    public void OnPointerUp()
    {
        _rectTransformToMove = null;
    }

    public void Drop()
    {
        if (!_isDragging) return;

        //TODO：落とした先の座標になにかあれば処理して抜ける

        //元に戻す
        _rectTransformToMove.SetParent(_prebParent);
        _rectTransformToMove.localPosition = _prebPosition;
        _isDragging = false;
        OnPointerUp();
    }

    public bool IsDragging => _isDragging;
}