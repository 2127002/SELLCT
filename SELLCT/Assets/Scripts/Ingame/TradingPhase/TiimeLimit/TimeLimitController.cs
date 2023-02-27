using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLimitController : MonoBehaviour
{
    [SerializeField] TimeLimit _timeLimit;

    Action _onTimeLimit;

    private void Update()
    {
        _timeLimit.DecreaseTimeDeltaTime();

        if (_timeLimit.IsTimeLimitReached())
        {
            Debug.Log("êßå¿éûä‘Ç≈Ç∑");

            _onTimeLimit?.Invoke();
        }
    }

    public void AddAction(Action onTimeLimit)
    {
        _onTimeLimit += onTimeLimit;
    }

    public void RemoveAction(Action onTimeLimit)
    {
        _onTimeLimit -= onTimeLimit;
    }
}
