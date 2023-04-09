using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor01Answer02 : MonoBehaviour
{
    [SerializeField] E37_Lack _e37;
    [SerializeField] E7_Move _e7;
    [Header("���������m���m�P�ʁF���n")]
    [SerializeField, Range(0, 100f)] float defaultSuccessRate = 10f;
    [Header("�����m���̑�����l�m�P�ʁF���n")]
    [SerializeField, Range(0, 100f)] float successRateIncreasePercentage = 5f;
    [Header("�����܂ł̕K�v�C�x���g��")]
    [SerializeField, Min(0)] int requiredEventCount = 3;
    [SerializeField] TextBoxController _textBoxController;
    [SerializeField] PhaseController _phaseController;
    float successRate;
    int eventCount = 0;

    void Reset()
    {
        _e37 = FindObjectOfType<E37_Lack>();
        _e7 = FindObjectOfType<E7_Move>();
        _textBoxController = FindObjectOfType<TextBoxController>();
        _phaseController = FindObjectOfType<PhaseController>();
    }

    private void Awake()
    {
        successRate = defaultSuccessRate;
    }

    private bool HasElements()
    {
        return _e37.ContainsPlayerDeck && _e7.ContainsPlayerDeck;
    }

    public void Go()
    {
        if (!HasElements()) return;

        //�����m��15%�Ȃ�A0�`14.9999...�Ȃ琬��
        float rate = UnityEngine.Random.Range(0, 100f);

        //���s
        if (rate >= successRate)
        {
            successRate += successRateIncreasePercentage;
            return;
        }

        //����
        eventCount++;
        if (eventCount >= requiredEventCount)
        {
            _phaseController.OnExplorationPhaseStart += Succeed;
        }
        else
        {
            _phaseController.OnExplorationPhaseStart += Hint;
        }
    }

    private async void Succeed()
    {
        try
        {
            //�N���A
            await _textBoxController.UpdateText(null, "...!!\n�^�ǂ���ɑ������𔭌������B");
            await _textBoxController.UpdateText(null, "...�i��ł݂邩�B");
        }
        catch (OperationCanceledException)
        {
            //�L�����Z�����ꂽ�ꍇ�͏������I������
            return;
        }
        finally
        {
            _phaseController.OnExplorationPhaseStart -= Succeed;
        }
    }

    private async void Hint()
    {
        try
        {
            await _textBoxController.UpdateText(null, "...�Ȃ񂾂낤�A���̕ǁB���������肻���ȗ\��������");
        }
        catch (OperationCanceledException)
        {
            //�L�����Z�����ꂽ�ꍇ�͏������I������
            return;
        }
        finally
        {
            _phaseController.OnExplorationPhaseStart -= Hint;
        }
    }
}