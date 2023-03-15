using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TradingNextButtonHandler : MonoBehaviour
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

    [SerializeField] bool _isFirstSelectable;

    private void Awake()
    {
        //�w��
        _clickDetector.AddListener(HandleClick);
        _enterDetector.AddListener(HandleEnter);
        _exitDetector.AddListener(HandleExit);
        _submitDetector.AddListener(HandleSubmit);
        _selectDetector.AddListener(HandleSelect);
        _phaseController.onTradingPhaseStart += SetFirstSelectable;

        //�킩��₷�����邽�߉��ɑI�����̐F��ԂɕύX�B����̕ύX����
        var b = _selectable.colors;
        b.selectedColor = Color.red;
        _selectable.colors = b;
    }

    private void OnDestroy()
    {
        //�w�ǉ���
        _clickDetector.RemoveListener(HandleClick);
        _enterDetector.RemoveListener(HandleEnter);
        _exitDetector.RemoveListener(HandleExit);
        _submitDetector.RemoveListener(HandleSubmit);
        _selectDetector.RemoveListener(HandleSelect);
        _phaseController.onTradingPhaseStart -= SetFirstSelectable;
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
        //�t�F�[�Y�I����m�点��
        _phaseController.CompleteTradingPhase();
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
        //���ꏈ���̂��߈ȉ��̏������ĂԂ����ɂ��܂��B�N���b�N���̎d�l�ƍ��ق�����������C�����Ă��������B
        HandleClick();
    }

    private void HandleSelect()
    {
        //TODO:SE1�̍Đ�
    }
}
