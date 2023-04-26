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
        //���N���b�N�ȊO�s��Ȃ�
        if (eventData.button != PointerEventData.InputButton.Left) return;

        onClick?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        throw new NotImplementedException();
    }
}
