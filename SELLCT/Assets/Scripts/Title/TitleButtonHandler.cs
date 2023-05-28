using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TitleButtonHandler : MonoBehaviour, ISubmitHandler, ISelectHandler, IDeselectHandler
{
    [SerializeField] TextMeshProUGUI _textMeshProUGUI = default!;
    [SerializeField] UnityEvent onSubmit = default!;

    void ISubmitHandler.OnSubmit(BaseEventData eventData)
    {
        onSubmit?.Invoke();
    }

    void ISelectHandler.OnSelect(BaseEventData eventData)
    {
        _textMeshProUGUI.fontSize = 43.5f;
    }

    void IDeselectHandler.OnDeselect(BaseEventData eventData)
    {
        _textMeshProUGUI.fontSize = 35f;
    }
}

