using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

/// <summary>
/// 売買フェーズの進行に責任を持つクラス
/// </summary>
public class TradingPhaseController : MonoBehaviour
{
    [SerializeField] TimeLimitController _timeLimitController;
    [SerializeField] HandMediator _handMediator;

    ITradingPhaseViewReceiver _view;
    EventSystem _eventSystem;
    GameObject _lastSelectedObject;

    private void Awake()
    {
        _view = GetComponent<ITradingPhaseViewReceiver>();
        _timeLimitController.AddAction(OnTimeLimit);

        _eventSystem = EventSystem.current;

        //プレイヤー手札の配布
        _handMediator.TakeCard();
    }

    private void Start()
    {
        //現在選択中のUIオブジェクトを登録。
        //選択中オブジェクトの初期化処理がAwakeで行われるためStartに配置しています。
        _lastSelectedObject = _eventSystem.currentSelectedGameObject;
    }

    private void OnEnable()
    {
        InputSystemDetector.Instance.AddNavigate(OnNavigate);
    }

    private void OnDisable()
    {
        InputSystemDetector.Instance.RemoveNavigate(OnNavigate);
    }

    private void OnTimeLimit()
    {
        _view.OnTimeLimitReached();
    }

    private void OnNavigate(InputAction.CallbackContext context)
    {
        if (_eventSystem.currentSelectedGameObject != null)
        {
            _lastSelectedObject = _eventSystem.currentSelectedGameObject;
            return;
        }

        //未選択時のみ実行
        _eventSystem.SetSelectedGameObject(_lastSelectedObject);
    }
}