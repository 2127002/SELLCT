using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerEnterDetector : MonoBehaviour, IPointerEnterHandler
{
    Action onPointerEnter;

    public void AddListener(Action action)
    {
        onPointerEnter += action;
    }

    public void RemoveListener(Action action)
    {
        onPointerEnter -= action;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        onPointerEnter?.Invoke();
    }
}
