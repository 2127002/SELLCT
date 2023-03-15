using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameController : MonoBehaviour
{
    [SerializeField] IngameView _view = default!;
    [SerializeField] PhaseController _phaseController;

    private void Awake()
    {
        _phaseController.onGameStart += _view.OnGameStart;
    }

    private void OnDestroy()
    {
        _phaseController.onGameStart -= _view.OnGameStart;
    }
}
