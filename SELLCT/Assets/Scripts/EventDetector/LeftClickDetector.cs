using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class LeftClickDetector : MonoBehaviour, IPointerClickHandler
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

    public void OnPointerClick(PointerEventData eventData)
    {
        //左クリック以外行わない
        if (eventData.button != PointerEventData.InputButton.Left) return;

        onClick?.Invoke();
    }
}
