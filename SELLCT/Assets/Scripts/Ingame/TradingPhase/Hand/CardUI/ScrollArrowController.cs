using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollArrowController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, ISubmitHandler, ISelectHandler, IDeselectHandler
{
    enum Direction
    {
        Up = 0,
        Down = 1,

        [InspectorName("")]
        Invalid,
    }

    [SerializeField] Direction _direction = default!;

    [SerializeField] RectTransform _cardsParent = default!;

    //288+60
    const float CARDHEIGHT = 348f;
    static readonly List<Vector3> offset = new() { new Vector3(0, -CARDHEIGHT, 0), new Vector3(0, CARDHEIGHT, 0) };

    public void OnDeselect(BaseEventData eventData)
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Up");

        Submit();
    }

    public void OnSelect(BaseEventData eventData)
    {
    }

    public void OnSubmit(BaseEventData eventData)
    {
        Submit();
    }

    private void Submit()
    {
        Debug.Log("Submit");

        _cardsParent.localPosition += offset[(int)_direction];
    }
}
