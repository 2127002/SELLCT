using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragAndDropController
{
    //�J�[�\����Transform
    RectTransform _cursorRectTransform = default!;

    //D&D�O�̏��
    Vector3 _prebPosition = default!;
    Transform _prebParent = default!;

    //�h���b�O����Transform
    RectTransform _rectTransformToMove = default!;

    bool _isDragging = false;

    public DragAndDropController(RectTransform cursorRectTransform)
    {
        _cursorRectTransform = cursorRectTransform;
    }

    public void OnPointerDown(RectTransform rectTransformToMove)
    {
        if (rectTransformToMove == null) throw new System.ArgumentNullException("����������Transform��null�ł�");

        //D&D�O�̏���ۑ�
        _prebPosition = rectTransformToMove.localPosition;
        _prebParent = rectTransformToMove.parent;
        _rectTransformToMove = rectTransformToMove;
    }

    public void OnCursorMoving()
    {
        if (_isDragging) return;
        if (_rectTransformToMove == null) return;

        //�J�[�\����e�ɂ��ăh���b�O��Ԃɂ���
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

        //TODO�F���Ƃ�����̍��W�ɂȂɂ�����Ώ������Ĕ�����

        //���ɖ߂�
        _rectTransformToMove.SetParent(_prebParent);
        _rectTransformToMove.localPosition = _prebPosition;
        _isDragging = false;
        OnPointerUp();
    }

    public bool IsDragging => _isDragging;
}