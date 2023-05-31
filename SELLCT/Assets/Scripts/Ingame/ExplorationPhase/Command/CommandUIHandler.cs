using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CommandUIHandler : MonoBehaviour, ISelectableHighlight, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, ISubmitHandler, ISelectHandler, IDeselectHandler
{
    //�I����
    [SerializeField] Selectable _selectable = default!;

    [SerializeField] PhaseController _phaseController = default!;

    [SerializeField] Card _card = default!;

    Color _defaultSelectColor = default!;

    private void Awake()
    {
        _phaseController.OnExplorationPhaseStart += OnPhaseStart;

        _defaultSelectColor = _selectable.colors.selectedColor;
    }

    private void OnPhaseStart()
    {
        if (_card == null)
        {
            Debug.LogWarning("U6�G�������g�J�[�h��null�ł�"); return;
        }

        bool containsCard = _card.ContainsPlayerDeck;

        //Grid Layout Group�ŊǗ����Ă��邽�߁AImage��Enable�ł͂Ȃ�GameObject����I���I�t���܂�
        gameObject.SetActive(containsCard);
    }

    private void OnSubmit()
    {
        //U6�p�̏���
        _card.OnPressedU6Button();
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

    public void OnDeselect(BaseEventData eventData)
    {
    }

    public void OnSelect(BaseEventData eventData)
    {
    }

    public void OnSubmit(BaseEventData eventData)
    {
        //���ꏈ���̂��߈ȉ��̏������ĂԂ����ɂ��܂��B�N���b�N���̎d�l�ƍ��ق�����������C�����Ă��������B
        OnSubmit();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //TODO�F���ケ���ɋ�̓I�ȃJ�[�\�������������ۂ̏�����ǉ�����
        _selectable.Select();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //���ꏈ���̂��߈ȉ��̏������ĂԂ����ɂ��܂��BSubmit���̎d�l�ƍ��ق�����������C�����Ă��������B
        OnSubmit();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //TODO�F���ケ���ɋ�̓I�ȃJ�[�\�����O�����ۂ̏�����ǉ�����
        EventSystem.current.SetSelectedGameObject(null);
    }
}
