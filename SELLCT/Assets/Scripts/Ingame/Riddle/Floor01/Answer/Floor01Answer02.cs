using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor01Answer02 : MonoBehaviour
{
    [SerializeField] E37_Lack _e37;
    [SerializeField] E7_Move _e7;
    [Header("初期成功確率［単位：％］")]
    [SerializeField, Range(0, 100f)] float defaultSuccessRate = 10f;
    [Header("成功確率の増える値［単位：％］")]
    [SerializeField, Range(0, 100f)] float successRateIncreasePercentage = 5f;
    [Header("成功までの必要イベント数")]
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

        //成功確率15%なら、0～14.9999...なら成功
        float rate = UnityEngine.Random.Range(0, 100f);

        //失敗
        if (rate >= successRate)
        {
            successRate += successRateIncreasePercentage;
            return;
        }

        //成功
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
            //クリア
            await _textBoxController.UpdateText(null, "...!!\n運良く先に続く道を発見した。");
            await _textBoxController.UpdateText(null, "...進んでみるか。");
        }
        catch (OperationCanceledException)
        {
            //キャンセルされた場合は処理を終了する
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
            await _textBoxController.UpdateText(null, "...なんだろう、この壁。何ががありそうな予感がする");
        }
        catch (OperationCanceledException)
        {
            //キャンセルされた場合は処理を終了する
            return;
        }
        finally
        {
            _phaseController.OnExplorationPhaseStart -= Hint;
        }
    }
}