using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ExplorationPhaseController : MonoBehaviour
{
    [SerializeField] PhaseController _phaseController = default!;
    [SerializeField] Canvas _canvas = default!;
    [SerializeField] Selectable _firstSelectable = default!;
    [SerializeField] InputActionReference _navigateAction = default!;

    GameObject _lastSelectedObject = default!;

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

    private void OnPhaseStart()
    {
        //キャンバスのenabledを変更するだけではSelectableが反応してしまうためGameObjectのActiveを変更します。
        _canvas.gameObject.SetActive(true);

        _navigateAction.action.performed += OnNavigate;

        //初期のSelect設定
        EventSystem.current.SetSelectedGameObject(_firstSelectable.gameObject);
        _lastSelectedObject = _firstSelectable.gameObject;
        //TODO:BGM2の再生
    }

    private void OnPhaseComplete()
    {
        //キャンバスのenabledを変更するだけではSelectableが反応してしまうためGameObjectのActiveを変更します。
        _canvas.gameObject.SetActive(false);

        _navigateAction.action.performed -= OnNavigate;
    }

    private void OnNavigate(InputAction.CallbackContext context)
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            _lastSelectedObject = EventSystem.current.currentSelectedGameObject;
            return;
        }

        //未選択時のみ実行
        EventSystem.current.SetSelectedGameObject(_lastSelectedObject);
    }
}
