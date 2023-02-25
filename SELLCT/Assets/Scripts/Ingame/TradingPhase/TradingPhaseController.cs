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
            Debug.Log("�������Ԃł�");

            _view.OnTimeLimitReached();

            //TODO�F��D�����Z�b�g
            //TODO�F�e�L�X�g�{�b�N�X�̍X�V
        }
    }
}
