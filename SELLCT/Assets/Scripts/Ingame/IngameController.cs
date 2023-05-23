using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameController : MonoBehaviour
{
    [SerializeField] IngameView _view = default!;
    [SerializeField] PhaseController _phaseController = default!;

    private void Awake()
    {
        _phaseController.OnGameStart.Add(_view.OnGameStart);
    }

    private void OnDestroy()
    {
        _phaseController.OnGameStart.Remove(_view.OnGameStart);
    }
}
