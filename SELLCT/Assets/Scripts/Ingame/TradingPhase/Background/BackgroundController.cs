using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] BackgroundView _backgroundView = default!;
    [SerializeField] PhaseController _phaseController = default!;
    
    private void Awake()
    {
        _phaseController.OnTradingPhaseStart.Add(OnPhaseStart);
    }

    private void OnDestroy()
    {
        _phaseController.OnTradingPhaseStart.Remove(OnPhaseStart);
    }

    private void OnPhaseStart()
    {
        _backgroundView.OnPhaseStart();
    }
}
