using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// AボタンやEnterキーなどの決定に反応
/// </summary>
public class SubmitDetector : MonoBehaviour, ISubmitHandler
{
    Action onSubmit;

    public void AddListener(Action action)
    {
        onSubmit += action;
    }

    public void RemoveListener(Action action)
    {
        onSubmit -= action;
    }

    public void OnSubmit(BaseEventData eventData)
    {
        onSubmit?.Invoke();
    }
}
