using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLimitController : MonoBehaviour
{
    [SerializeField, Min(0)] int _timeLimitValueInSeconds;
    [SerializeField] PhaseController _phaseController = default!;

    TimeLimit _timeLimit;

    public event Action OnTimeLimit;

    private void Awake()
    {
        _phaseController.OnTradingPhaseStart.Add(OnPhaseStart);
        _phaseController.OnTradingPhaseComplete.Add(OnPhaseComplete);
    }

    private void OnDestroy()
    {
        _phaseController.OnTradingPhaseStart.Remove(OnPhaseStart);
        _phaseController.OnTradingPhaseComplete.Remove(OnPhaseComplete);
    }

    private void OnPhaseStart()
    {
        _timeLimit = new(_timeLimitValueInSeconds);
    }

    private async UniTask OnPhaseComplete()
    {
        var token = this.GetCancellationTokenOnDestroy();

        _timeLimit = null;

        await UniTask.Yield(token);
    }

    private void Update()
    {
        if (_timeLimit != null) _timeLimit.DecreaseTimeDeltaTime();
    }

    private void FixedUpdate()
    {
        if (_timeLimit != null) TimeLimitChecker();
    }

    private void TimeLimitChecker()
    {
        if (!_timeLimit.IsTimeLimitReached()) return;

        Debug.Log("制限時間です");

        OnTimeLimit?.Invoke();

        //制限時間になったらスクリプトごと無効にする
        enabled = false;
    }
}
