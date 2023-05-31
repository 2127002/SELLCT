using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Name : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TextBoxController _textBoxController;

    public void OnPointerDown(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }

    public async void OnPointerUp(PointerEventData eventData)
    {
        await _textBoxController.UpdateText(null, "見つけた。己を示す一冊の本を。");
        await _textBoxController.UpdateText(null, "...そうか、初めから答えはそこにあったんだな。");
        await _textBoxController.UpdateText(null, "Fin....                                 でいいよな？");
    }
}
