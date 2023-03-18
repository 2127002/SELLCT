using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplorationPhaseController : MonoBehaviour
{
    [SerializeField] PhaseController _phaseController;
    [SerializeField] Canvas _canvas;

    private void Awake()
    {
        _phaseController.OnExplorationPhaseStart += OnPhaseStart;
        _phaseController.OnExplorationPhaseComplete += OnPhaseComplete;
    }

    private void OnPhaseComplete()
    {
        _canvas.enabled = false;
    }

    private void OnPhaseStart()
    {
        _canvas.enabled = true;
        //TODO:BGM2ÇÃçƒê∂
    }
}
