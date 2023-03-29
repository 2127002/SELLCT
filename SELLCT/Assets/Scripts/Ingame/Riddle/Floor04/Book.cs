using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Book : MonoBehaviour,IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
 [SerializeField] E11_Vivid _e11;

    [SerializeField] Canvas _canvas;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_e11.FindAll != 0) return;
        Debug.Log("Ž©•ª‚Ì–{‚ðŒ©‚Â‚¯‚½");
        _canvas.enabled = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }
}
