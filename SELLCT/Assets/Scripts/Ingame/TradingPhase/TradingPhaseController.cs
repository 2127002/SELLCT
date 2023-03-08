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
    [SerializeField] TradingPhaseView _view;
    [SerializeField] TradingPhaseCompletionHandler _completionHandler;
    [SerializeField] HandMediator _handMediator;
    [SerializeField] TextBoxView _textBoxView;
    [SerializeField] TraderController _traderController;

    EventSystem _eventSystem;
    GameObject _lastSelectedObject;

    private void Awake()
    {
        _completionHandler.AddListener(OnComplete);

        _eventSystem = EventSystem.current;

        //プレイヤー手札の配布
        _handMediator.InitTakeCard();
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

    private async void OnComplete()
    {
        //テキストの表示
        _textBoxView.UpdeteText(_traderController.CurrentTrader.EndMessage());
        await _textBoxView.DisplayTextOneByOne();

        //フェードアウト
        await _view.StartFadeout();

        //TODO：BGM1のフェードアウト
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