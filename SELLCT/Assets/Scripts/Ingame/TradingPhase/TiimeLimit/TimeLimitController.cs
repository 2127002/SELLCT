using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLimitController : MonoBehaviour
{
    [Header("�ő�l�Ə����l�iE24�̏�����*���̒l�j�̌v�Z�Ɏg�p���܂��B")]
    [SerializeField, Min(0)] float _timeLimitRate;

    [SerializeField] PhaseController _phaseController = default!;

    TimeLimit _timeLimit;

    public event Action OnTimeLimit;

    private void Awake()
    {
        _phaseController.OnTradingPhaseComplete.Add(OnPhaseComplete);
    }

    private void OnDestroy()
    {
        _phaseController.OnTradingPhaseComplete.Remove(OnPhaseComplete);
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

        Debug.Log("�������Ԃł�");

        OnTimeLimit?.Invoke();
        _timeLimit = null;
    }

    public void Generate(int currentE24Count)
    {
        _timeLimit = new(currentE24Count * _timeLimitRate, _timeLimitRate);
    }

    public void AddTimeLimit(float value, int currentE24Count)
    {
        _timeLimit = _timeLimit.AddTimeLimit(new(value, _timeLimitRate), currentE24Count);
    }
    public void ReduceTimeLimit(float value, int currentE24Count)
    {
        _timeLimit = _timeLimit.ReduceTimeLimit(new(value, _timeLimitRate), currentE24Count);
    }
}
