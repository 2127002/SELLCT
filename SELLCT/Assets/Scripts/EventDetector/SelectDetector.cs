using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectDetector : MonoBehaviour, ISelectHandler
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

    public void OnSelect(BaseEventData eventData)
    {
        onClick?.Invoke();
    }
}
