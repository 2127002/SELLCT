using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerExitDetector : MonoBehaviour,IPointerExitHandler
{
    Action onPointerExit;

    public void AddListener(Action action)
    {
        onPointerExit += action;
    }

    public void RemoveListener(Action action)
    {
        onPointerExit -= action;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onPointerExit?.Invoke();
    }
}
