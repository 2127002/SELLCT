using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ExplorationNextButtonHandler : MonoBehaviour, ISelectableHighlight
{
    //���̂悤��Detector�ɂ킴�킴�����Ă���̂́Ainterface�̃��\�b�h��public�ɂȂ邩��ł��B
    //�O������Ӑ}���Ȃ��^�C�~���O�ŌĂ΂�邱�Ƃ�����邽�߉�肭�ǂ�����g���Ă��܂��B
    [SerializeField] LeftClickDetector _clickDetector = default!;
    [SerializeField] PointerEnterDetector _enterDetector = default!;
    [SerializeField] PointerExitDetector _exitDetector = default!;
    [SerializeField] SubmitDetector _submitDetector = default!;
    [SerializeField] SelectDetector _selectDetector = default!;
    [SerializeField] Selectable _selectable = default!;

    [SerializeField] PhaseController _phaseController = default!;
    [SerializeField] Floor01Condition _floor01 = default!;

    Color _defaultSelectColor = default!;

    private void Reset()
    {
        _clickDetector = GetComponent<LeftClickDetector>();
        _enterDetector = GetComponent<PointerEnterDetector>();
        _exitDetector = GetComponent<PointerExitDetector>();
        _submitDetector = GetComponent<SubmitDetector>();
        _selectDetector = GetComponent<SelectDetector>();
        _selectable = GetComponent<Selectable>();
        _phaseController = FindObjectOfType<PhaseController>();
        _floor01 = FindObjectOfType<Floor01Condition>();
    }

    private void Awake()
    {
        //�w��
        _clickDetector.AddListener(HandleClick);
        _enterDetector.AddListener(HandleEnter);
        _exitDetector.AddListener(HandleExit);
        _submitDetector.AddListener(HandleSubmit);
        _selectDetector.AddListener(HandleSelect);

        _defaultSelectColor = _selectable.colors.selectedColor;
    }

    private void OnDestroy()
    {
        //�w�ǉ���
        _clickDetector.RemoveListener(HandleClick);
        _enterDetector.RemoveListener(HandleEnter);
        _exitDetector.RemoveListener(HandleExit);
        _submitDetector.RemoveListener(HandleSubmit);
        _selectDetector.RemoveListener(HandleSelect);
    }

    private void HandleClick()
    {
        OnSubmit();
    }

    private void OnSubmit()
    {
        //�t�F�[�Y�I����m�点��
        EventSystem.current.SetSelectedGameObject(null);

        _phaseController.CompleteExplorationPhase();
        _floor01.OnNextButtonPressed();
    }

    private void HandleEnter()
    {
        //TODO�F���ケ���ɋ�̓I�ȃJ�[�\�������������ۂ̏�����ǉ�����
        _selectable.Select();
    }

    private void HandleExit()
    {
        //TODO�F���ケ���ɋ�̓I�ȃJ�[�\�����O�����ۂ̏�����ǉ�����
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void HandleSubmit()
    {
        OnSubmit();
    }

    private void HandleSelect()
    {
        //TODO:SE1�̍Đ�
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
}
