using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class LeftClickDetector : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    Action onClick;

    public void AddListener(Action action)
    {
        onClick += action;
    }

    public void RemoveListener(Action action)
    {
        onClick -= action;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //左クリック以外行わない
        if (eventData.button != PointerEventData.InputButton.Left) return;

        onClick?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        throw new NotImplementedException();
    }
}
