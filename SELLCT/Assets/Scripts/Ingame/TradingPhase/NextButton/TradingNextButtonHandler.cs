using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TradingNextButtonHandler : MonoBehaviour, ISelectableHighlight, IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler,ISubmitHandler,ISelectHandler,IDeselectHandler
{
    [SerializeField] Selectable _selectable = default!;

    [SerializeField] PhaseController _phaseController = default!;

    Color _defalutSelectColor = default!;

    private void Awake()
    {
        _defalutSelectColor = _selectable.colors.selectedColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //TODO�F���ケ���ɋ�̓I�ȃJ�[�\�������������ۂ̏�����ǉ�����
        _selectable.Select();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //TODO�F���ケ���ɋ�̓I�ȃJ�[�\�����O�����ۂ̏�����ǉ�����
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Submit();
    }

    public void OnSubmit(BaseEventData eventData)
    {
        Submit();
    }

    private void Submit()
    {
        //�t�F�[�Y�I����m�点��
        _phaseController.CompleteTradingPhase();
    }

    public void OnSelect(BaseEventData eventData)
    {
        //TODO:SE1�̍Đ�
    }

    public void OnDeselect(BaseEventData eventData)
    {
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
