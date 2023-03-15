using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplorationPhaseController : MonoBehaviour
{
    [SerializeField] PhaseController _phaseController;

    private void Awake()
    {
        _phaseController.onExplorationPhaseStart += OnPhaseStart;
    }

    private void OnPhaseStart()
    {
        //TODO:BGM2ÇÃçƒê∂
    }
}
