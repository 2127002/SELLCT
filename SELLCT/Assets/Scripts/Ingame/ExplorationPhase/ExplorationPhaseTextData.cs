using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplorationPhaseTextData : MonoBehaviour
{
    [SerializeField] PhaseController _phaseController = default!;
    [SerializeField] TextBoxController _textBoxController = default!;

    string startText = "...�T���J�n���B�C�ɂȂ���͖̂������낤���H";

    private void Awake()
    {
        _phaseController.OnExplorationPhaseStart += OnExplorationPhaseStart;
    }

    private void OnExplorationPhaseStart()
    {
        _textBoxController.UpdateText(null, startText).Forget();
    }
}
