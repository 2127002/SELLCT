using Cysharp.Threading.Tasks;
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
    [SerializeField] TradingPhaseView _view = default!;
    [SerializeField] TextBoxView _textBoxView = default!;
    [SerializeField] TraderController _traderController = default!;
    [SerializeField] PhaseController _phaseController = default!;
    [SerializeField] TimeLimitController _timeLimitController = default!;
    [SerializeField] InputSystemDetector _inputSystemDetector = default!;
    [SerializeField] Canvas _canvas = default!;

    GameObject _lastSelectedObject = default!;

    private void Awake()
    {
        _phaseController.onTradingPhaseComplete.Add(OnComplete);
        _phaseController.onTradingPhaseStart += OnPhaseStart;
        _phaseController.onExplorationPhaseStart += OnExplorationPhaseStart;

        //タイムリミットになったらフェーズを完了する
        _timeLimitController.OnTimeLimit += _phaseController.CompleteTradingPhase;

        _inputSystemDetector.OnNavigateAction += OnNavigate;
    }

    private void OnExplorationPhaseStart()
    {
        _canvas.enabled = false;
    }

    private void Start()
    {
        //現在選択中のUIオブジェクトを登録。
        //選択中オブジェクトの初期化処理がAwakeで行われるためStartに配置しています。
        _lastSelectedObject = EventSystem.current.currentSelectedGameObject;
    }

    private void OnDestroy()
    {
        _phaseController.onTradingPhaseComplete.Remove(OnComplete);
        _phaseController.onTradingPhaseStart -= OnPhaseStart;
        _phaseController.onExplorationPhaseStart -= OnExplorationPhaseStart;

        _timeLimitController.OnTimeLimit -= _phaseController.CompleteTradingPhase;
        _inputSystemDetector.OnNavigateAction -= OnNavigate;
    }

    //売買フェーズ開始時処理
    private void OnPhaseStart()
    {
        _view.OnPhaseStart();
        _canvas.enabled = true;
    }

    //売買フェーズ終了時処理（待機可）
    private async UniTask OnComplete()
    {
        //テキストの表示
        _textBoxView.UpdeteText(_traderController.CurrentTrader.EndMessage());
        await _textBoxView.DisplayTextOneByOne();

        //フェードアウト
        await _view.OnPhaseComplete();

        //TODO：BGM1のフェードアウト
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