using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Book : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] E11_Vivid _e11;
    [SerializeField] Canvas _canvas;

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_e11.FindAll != 0) return;
        Debug.Log("�����̖{��������");
        _canvas.enabled = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
