using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Name : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TextBoxController _textBoxController;

    public async void OnPointerClick(PointerEventData eventData)
    {
        await _textBoxController.UpdateText(null, "�������B�Ȃ���������̖{���B");
        await _textBoxController.UpdateText(null, "...�������A���߂��瓚���͂����ɂ������񂾂ȁB");
        await _textBoxController.UpdateText(null, "Fin....                                 �ł�����ȁH");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }
}
