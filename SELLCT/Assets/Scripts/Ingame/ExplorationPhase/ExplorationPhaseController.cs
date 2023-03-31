using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ExplorationPhaseController : MonoBehaviour
{
    [SerializeField] PhaseController _phaseController;
    [SerializeField] Canvas _canvas;

    private void Awake()
    {
        _phaseController.OnGameStart.Add(OnGameStart);
        _phaseController.OnExplorationPhaseStart += OnPhaseStart;
        _phaseController.OnExplorationPhaseComplete += OnPhaseComplete;
    }

    private void OnGameStart()
    {
        //キャンバスのenabledを変更するだけではSelectableが反応してしまうためGameObjectのActiveを変更します。
        _canvas.gameObject.SetActive(false);
    }

    private void OnPhaseComplete()
    {
        //キャンバスのenabledを変更するだけではSelectableが反応してしまうためGameObjectのActiveを変更します。
        _canvas.gameObject.SetActive(false);
    }

    private void OnPhaseStart()
    {
        //キャンバスのenabledを変更するだけではSelectableが反応してしまうためGameObjectのActiveを変更します。
        _canvas.gameObject.SetActive(true);

        //TODO:BGM2の再生
    }
}
