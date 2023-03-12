using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLimitController : MonoBehaviour
{
    [SerializeField] TimeLimit _timeLimit;
    [SerializeField] TradingPhaseCompletionHandler _phaseCompletionHandler;

    Action _onTimeLimit;

    private void Update()
    {
        _timeLimit.DecreaseTimeDeltaTime();
    }

    private void FixedUpdate()
    {
        TimeLimitChecker();
    }

    private void TimeLimitChecker()
    {
        if (!_timeLimit.IsTimeLimitReached()) return;

        Debug.Log("�������Ԃł�");

        _onTimeLimit?.Invoke();
        _phaseCompletionHandler.OnComplete();

        //�������ԂɂȂ�����X�N���v�g���Ɩ����ɂ���
        enabled = false;
    }

    public void AddListener(Action onTimeLimit)
    {
        _onTimeLimit += onTimeLimit;
    }

    public void RemoveListener(Action onTimeLimit)
    {
        _onTimeLimit -= onTimeLimit;
    }
}
