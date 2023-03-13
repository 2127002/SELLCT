using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeselectDetector : MonoBehaviour, IDeselectHandler
{
    Action onDeselect;

    public void AddListener(Action action)
    {
        onDeselect += action;
    }

    public void RemoveListener(Action action)
    {
        onDeselect -= action;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        onDeselect?.Invoke();
    }
}
