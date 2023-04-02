using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConversationChoiceHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISubmitHandler, IPointerClickHandler, ISelectableHighlight
{
    ITalkChoice _talkChoice = default!;

    [SerializeField] TextMeshProUGUI _text = default!;
    [SerializeField] Selectable _selectable = default!;
    Color _defalutSelectColor;

    private void Awake()
    {
        _defalutSelectColor = _selectable.colors.selectedColor;
    }

    public void SetTalkChoice(ITalkChoice talkChoice)
    {
        _talkChoice = talkChoice;

        if (talkChoice is NullChoice)
        {
            gameObject.SetActive(false);
            return;
        }

        if (talkChoice is NothingChoice)
        {
            if (!talkChoice.Enabled)
            {
                gameObject.SetActive(false);
                return;
            }
        }

        gameObject.SetActive(true);

        _text.text = _talkChoice.Text.ToDisplayString();
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

    public void EnableHighlight()
    {
        var selectableColors = _selectable.colors;
        selectableColors.selectedColor = _defalutSelectColor;
        _selectable.colors = selectableColors;
    }

    public void DisableHighlight()
    {
        var selectableColors = _selectable.colors;

        //���̐F��ۑ����Ă���
        _defalutSelectColor = selectableColors.selectedColor;

        //�n�C���C�g������
        //���ۂ̓n�C���C�g�F��ʏ�F�ɕς��Ă邾��
        selectableColors.selectedColor = selectableColors.normalColor;
        _selectable.colors = selectableColors;
    }
}
