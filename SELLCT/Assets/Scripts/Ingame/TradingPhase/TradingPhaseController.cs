using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradingPhaseController : MonoBehaviour
{
    [SerializeField] TimeLimit _timeLimit;

    ITradingPhaseViewReceiver _view;

    private void Awake()
    {
        _view = GetComponent<ITradingPhaseViewReceiver>();
    }

    private void Update()
    {
        _timeLimit.DecreaseTimeDeltaTime();

        if (_timeLimit.IsTimeLimitReached())
        {
            Debug.Log("制限時間です");

            _view.OnTimeLimitReached();

            //TODO：手札がリセット
            //TODO：テキストボックスの更新
        }
    }
}
