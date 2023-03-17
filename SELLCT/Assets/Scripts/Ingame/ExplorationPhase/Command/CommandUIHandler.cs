using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CommandUIHandler : MonoBehaviour
{
    //���̂悤��Detector�ɂ킴�킴�����Ă���̂́Ainterface�̃��\�b�h��public�ɂȂ邩��ł��B
    //�O������Ӑ}���Ȃ��^�C�~���O�ŌĂ΂�邱�Ƃ�����邽�߉�肭�ǂ�����g���Ă��܂��B
    [SerializeField] LeftClickDetector _clickDetector = default!;
    [SerializeField] PointerEnterDetector _enterDetector = default!;
    [SerializeField] PointerExitDetector _exitDetector = default!;
    [SerializeField] SubmitDetector _submitDetector = default!;
    [SerializeField] SelectDetector _selectDetector = default!;
    [SerializeField] DeselectDetector _deselectDetector = default!;

    //�I����
    [SerializeField] Selectable _selectable = default!;

    [SerializeField] PhaseController _phaseController = default!;

    [SerializeField] Card _card = default!;

    private void Awake()
    {
        //�w��
        _clickDetector.AddListener(HandleClick);
        _enterDetector.AddListener(HandleEnter);
        _exitDetector.AddListener(HandleExit);
        _submitDetector.AddListener(HandleSubmit);
        _selectDetector.AddListener(HandleSelect);
        _deselectDetector.AddListener(HandleDeselect);

        _phaseController.OnExplorationPhaseStart += OnPhaseStart;

        //�킩��₷�����邽�߉��ɑI�����̐F��ԂɕύX�B����̕ύX����
        var b = _selectable.colors;
        b.selectedColor = Color.red;
        _selectable.colors = b;
    }

    private void OnPhaseStart()
    {
        bool containsCard = _card.ContainsPlayerDeck;

        //Grid Layout Group�ŊǗ����Ă��邽�߁AImage��Enable�ł͂Ȃ�GameObject����I���I�t���܂�
        gameObject.SetActive(containsCard);
    }

    private void OnDestroy()
    {
        //�w�ǉ���
        _clickDetector.RemoveListener(HandleClick);
        _enterDetector.RemoveListener(HandleEnter);
        _exitDetector.RemoveListener(HandleExit);
        _submitDetector.RemoveListener(HandleSubmit);
        _selectDetector.RemoveListener(HandleSelect);
        _deselectDetector.RemoveListener(HandleDeselect);
    }

    //�J�[�\�������������ۂ̏���
    private void HandleEnter()
    {
        //TODO�F���ケ���ɋ�̓I�ȃJ�[�\�������������ۂ̏�����ǉ�����
        _selectable.Select();
    }

    //�J�[�\�����O�����ۂ̏���
    private void HandleExit()
    {
        //TODO�F���ケ���ɋ�̓I�ȃJ�[�\�����O�����ۂ̏�����ǉ�����
        EventSystem.current.SetSelectedGameObject(null);
    }

    //���N���b�N������
    private void HandleClick()
    {
        //���ꏈ���̂��߈ȉ��̏������ĂԂ����ɂ��܂��BSubmit���̎d�l�ƍ��ق�����������C�����Ă��������B
        OnSubmit();
    }

    //���莞�̏���
    private void HandleSubmit()
    {
        //���ꏈ���̂��߈ȉ��̏������ĂԂ����ɂ��܂��B�N���b�N���̎d�l�ƍ��ق�����������C�����Ă��������B
        OnSubmit();
    }

    private void OnSubmit()
    {
        //U6�p�̏���
        _card.Passive();
    }

    //�I�����̏���
    private void HandleSelect()
    {
    }

    //�I������O�ꂽ���̏���
    private void HandleDeselect()
    {
    }
}
