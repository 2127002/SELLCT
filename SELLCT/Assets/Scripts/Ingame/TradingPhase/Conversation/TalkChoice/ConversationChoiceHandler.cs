using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConversationChoiceHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISubmitHandler, IPointerClickHandler
{
    ITalkChoice _talkChoice = default!;

    [SerializeField] TextMeshProUGUI _text = default!;
    [SerializeField] Selectable _selectable = default!;

    public void SetTalkChoice(ITalkChoice talkChoice)
    {
        _talkChoice = talkChoice;

        if(talkChoice is NullChoice)
        {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);

        _text.text = _talkChoice.Text;
        _selectable.interactable = _talkChoice.Enabled;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_selectable.interactable) return;

        _selectable.Select();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_selectable.interactable) return;

        EventSystem.current.SetSelectedGameObject(null);
    }

    public void OnSubmit(BaseEventData eventData)
    {
        Submit();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Submit();
    }

    private void Submit()
    {
        _talkChoice.Select();
    }
}
