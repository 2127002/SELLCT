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
    [SerializeField] TextBoxController _textBoxController = default!;
    [SerializeField] TraderController _traderController = default!;
    [SerializeField] PhaseController _phaseController = default!;
    [SerializeField] TimeLimitController _timeLimitController = default!;
    [SerializeField] InputSystemDetector _inputSystemDetector = default!;
    [SerializeField] CardUIHandler _firstSelectable = default!;
    [SerializeField] PlayerMonologue _playerMonologue = default!;
    [SerializeField] Canvas _canvas = default!;

    GameObject _lastSelectedObject = default!;

    private void Awake()
    {
        _phaseController.OnGameStart.Add(OnGameStart);
        _phaseController.OnTradingPhaseComplete.Add(OnComplete);
        _phaseController.OnTradingPhaseStart.Add(OnPhaseStart);

        //タイムリミットになったらフェーズを完了する
        _timeLimitController.OnTimeLimit += _phaseController.CompleteTradingPhase;

        _inputSystemDetector.OnNavigateAction += OnNavigate;
    }

    private void Start()
    {
        //現在選択中のUIオブジェクトを登録。
        //選択中オブジェクトの初期化処理がAwakeで行われるためStartに配置しています。
        _lastSelectedObject = EventSystem.current.currentSelectedGameObject;
    }

    private void OnDestroy()
    {
        _phaseController.OnGameStart.Remove(OnGameStart);
        _phaseController.OnTradingPhaseComplete.Remove(OnComplete);
        _phaseController.OnTradingPhaseStart.Remove(OnPhaseStart);

        _timeLimitController.OnTimeLimit -= _phaseController.CompleteTradingPhase;
        _inputSystemDetector.OnNavigateAction -= OnNavigate;
    }

    private void OnGameStart()
    {
        _canvas.gameObject.SetActive(false);
    }

    //売買フェーズ開始時処理
    private void OnPhaseStart()
    {
        _view.OnPhaseStart();
        EventSystem.current.SetSelectedGameObject(_firstSelectable.gameObject);
        _canvas.gameObject.SetActive(true);
    }

    //売買フェーズ終了時処理（待機可）
    private async UniTask OnComplete()
    {
        //プレイヤーの独白に置き換えるか判定する
        string speaker = _playerMonologue.SwitchToPlayerMonologue ?
            _playerMonologue.Speaker : _traderController.CurrentTrader.Name;

        string endMessage = _playerMonologue.SwitchToPlayerMonologue ?
            _playerMonologue.EndMessage() : _traderController.CurrentTrader.EndMessage();

        //テキストの表示
        await _textBoxController.UpdateText(speaker, endMessage);

        //フェードアウト
        await _view.OnPhaseComplete();

        _canvas.gameObject.SetActive(false);
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