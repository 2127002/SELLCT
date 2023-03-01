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
        //���N���b�N�ȊO�s��Ȃ�
        if (eventData.button != PointerEventData.InputButton.Left) return;

        onClick?.Invoke();
    }
}
