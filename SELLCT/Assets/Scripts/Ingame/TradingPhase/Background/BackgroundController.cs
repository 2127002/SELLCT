using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] BackgroundView _backgroundView = default!;
    [SerializeField] PhaseController _phaseController = default!;
    
    private void Awake()
    {
        _phaseController.onTradingPhaseStart += OnPhaseStart;
    }

    private void OnDestroy()
    {
        _phaseController.onTradingPhaseStart -= OnPhaseStart;
    }

    private void OnPhaseStart()
    {
        _backgroundView.OnPhaseStart();
    }
}
