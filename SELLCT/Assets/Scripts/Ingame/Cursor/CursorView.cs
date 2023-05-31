using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorView : MonoBehaviour
{
    [SerializeField] Image _cursorImage = default!;

    public Vector2 CursorSizeDelta => _cursorImage.rectTransform.sizeDelta;

    public void Enable()
    {
        enabled = true;
        _cursorImage.enabled = true;
    }

    public void Disable()
    {
        enabled = false;
        _cursorImage.enabled = false;
    }
}
