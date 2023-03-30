using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ExplorationNextButtonHandler : MonoBehaviour,ISelectableHighlight
{
    //���̂悤��Detector�ɂ킴�킴�����Ă���̂́Ainterface�̃��\�b�h��public�ɂȂ邩��ł��B
    //�O������Ӑ}���Ȃ��^�C�~���O�ŌĂ΂�邱�Ƃ�����邽�߉�肭�ǂ�����g���Ă��܂��B
    [SerializeField] LeftClickDetector _clickDetector;
    [SerializeField] PointerEnterDetector _enterDetector;
    [SerializeField] PointerExitDetector _exitDetector;
    [SerializeField] SubmitDetector _submitDetector;
    [SerializeField] SelectDetector _selectDetector;
    [SerializeField] Selectable _selectable;

    [SerializeField] bool _isFirstSelectable;

    [SerializeField] PhaseController _phaseController;
    [SerializeField] Floor01Condition _floor01;

    Color _defalutSelectColor = default!;

    private void Awake()
    {
        //�w��
        _clickDetector.AddListener(HandleClick);
        _enterDetector.AddListener(HandleEnter);
        _exitDetector.AddListener(HandleExit);
        _submitDetector.AddListener(HandleSubmit);
        _selectDetector.AddListener(HandleSelect);

        SetFirstSelectable();

        //�킩��₷�����邽�߉��ɑI�����̐F��ԂɕύX�B����̕ύX����
        _defalutSelectColor = _selectable.colors.selectedColor;
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

    private void SetFirstSelectable()
    {
        //�����I���̃`�F�b�N�{�b�N�X��true��������o�^
        if (!_isFirstSelectable) return;

        if (EventSystem.current.currentSelectedGameObject != null)
        {
            Debug.LogWarning("���łɕʂ̃I�u�W�F�N�g���I������Ă��܂��B" + gameObject + "�̓o�^�͊��p����܂����B�������d�l���m�F���Ă��������B" + EventSystem.current.currentSelectedGameObject);
            return;
        }

        EventSystem.current.SetSelectedGameObject(_selectable.gameObject);
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
