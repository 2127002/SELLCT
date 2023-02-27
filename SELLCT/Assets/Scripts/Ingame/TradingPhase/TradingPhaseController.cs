using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradingPhaseController : MonoBehaviour
{
    [SerializeField] TimeLimitController _timeLimitController;

    ITradingPhaseViewReceiver _view;

    private void Awake()
    {
        _view = GetComponent<ITradingPhaseViewReceiver>();
        _timeLimitController.AddAction(OnTimeLimit);
    }

    private void OnTimeLimit()
    {
        _view.OnTimeLimitReached();
    }
}