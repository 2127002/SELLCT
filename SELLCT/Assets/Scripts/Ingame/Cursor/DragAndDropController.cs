using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragAndDropController
{
    RectTransform _cursorRectTransform = default!;

    Vector3 _prebPosition = default!;
    Transform _prebParent = default!;

    bool _isDragging = false;

    public DragAndDropController(RectTransform cursorRectTransform)
    {
        _cursorRectTransform = cursorRectTransform;
    }

    public void OnPointerDown(RectTransform rectTransformToMove)
    {
        if (rectTransformToMove == null) throw new System.ArgumentNullException("“®‚©‚µ‚½‚¢Transform‚ªnull‚Å‚·");

        _prebPosition = rectTransformToMove.localPosition;
        _prebParent = rectTransformToMove.parent;
        rectTransformToMove.SetParent(_cursorRectTransform);
        rectTransformToMove.localPosition = Vector3.zero;
    }

    public void Drop(RectTransform rectTransformToMove)
    {
        //TODOF—Ž‚Æ‚µ‚½æ‚ÌÀ•W‚É‚È‚É‚©‚ ‚ê‚Îˆ—‚µ‚Ä”²‚¯‚é

        //Œ³‚É–ß‚·
        rectTransformToMove.localPosition = _prebPosition;
        rectTransformToMove.SetParent(_prebParent);
    }

    public bool IsDragging => _isDragging;
}