using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplorationPhaseTextData : MonoBehaviour
{
    [SerializeField] PhaseController _phaseController = default!;
    [SerializeField] TextBoxController _textBoxController = default!;

    string startText = "...探索開始だ。気になるものは無いだろうか？";

    private void Awake()
    {
        _phaseController.OnExplorationPhaseStart += OnExplorationPhaseStart;
    }

    private void OnExplorationPhaseStart()
    {
        _textBoxController.UpdateText(null, startText).Forget();
    }
}
