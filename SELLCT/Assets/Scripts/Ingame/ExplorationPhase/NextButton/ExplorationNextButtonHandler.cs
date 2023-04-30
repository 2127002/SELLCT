using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ExplorationNextButtonHandler : MonoBehaviour, ISelectableHighlight, IPointerUpHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, ISubmitHandler, ISelectHandler, IDeselectHandler
{
    [SerializeField] Selectable _selectable = default!;

    [SerializeField] PhaseController _phaseController = default!;
    [SerializeField] Floor01Condition _floor01 = default!;

    Color _defaultSelectColor = default!;

    private void Reset()
    {
        _selectable = GetComponent<Selectable>();
        _phaseController = FindObjectOfType<PhaseController>();
        _floor01 = FindObjectOfType<Floor01Condition>();
    }

    private void Awake()
    {
        _defaultSelectColor = _selectable.colors.selectedColor;
    }

    private void OnSubmit()
    {
        //�t�F�[�Y�I����m�点��
        EventSystem.current.SetSelectedGameObject(null);

        _phaseController.CompleteExplorationPhase();
        _floor01.OnNextButtonPressed();
    }

    public void EnableHighlight()
    {
        var selectableColors = _selectable.colors;
        selectableColors.selectedColor = _defaultSelectColor;
        _selectable.colors = selectableColors;
    }

    public void DisableHighlight()
    {
        var selectableColors = _selectable.colors;

        //���̐F��ۑ����Ă���
        _defaultSelectColor = selectableColors.selectedColor;

        //�n�C���C�g������
        //���ۂ̓n�C���C�g�F��ʏ�F�ɕς��Ă邾��
        selectableColors.selectedColor = selectableColors.normalColor;
        _selectable.colors = selectableColors;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnSubmit();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
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

    public void OnSubmit(BaseEventData eventData)
    {
        OnSubmit();
    }

    public void OnSelect(BaseEventData eventData)
    {
        //TODO:SE1�̍Đ�
    }

    public void OnDeselect(BaseEventData eventData)
    {
    }
}
