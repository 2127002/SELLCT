using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// 売買フェーズの進行に責任を持つクラス
/// </summary>
public class TradingPhaseController : MonoBehaviour
{
    [SerializeField] PhaseController _phaseController = default!;
    [SerializeField] TimeLimitController _timeLimitController = default!;
    [SerializeField] InputActionReference _navigateAction = default!;
    [SerializeField] Selectable _firstSelectable = default!;
    [SerializeField] ConversationController _conversationController = default!;
    [SerializeField] RugController _rugController = default!;
    [SerializeField] Canvas _canvas = default!;
    [SerializeField] InputActionReference _any = default!;

    GameObject _lastSelectedObject = default!;

    private void Awake()
    {
        _phaseController.OnGameStart.Add(OnGameStart);
        _phaseController.OnTradingPhaseComplete.Add(OnComplete);
        _phaseController.OnTradingPhaseStart.Add(OnPhaseStart);

        //タイムリミットになったらフェーズを完了する
        _timeLimitController.OnTimeLimit += _phaseController.CompleteTradingPhase;
    }

    private void OnDestroy()
    {
        _phaseController.OnGameStart.Remove(OnGameStart);
        _phaseController.OnTradingPhaseComplete.Remove(OnComplete);
        _phaseController.OnTradingPhaseStart.Remove(OnPhaseStart);

        _timeLimitController.OnTimeLimit -= _phaseController.CompleteTradingPhase;
    }

    private void OnGameStart()
    {
        _canvas.gameObject.SetActive(false);
    }

    //売買フェーズ開始時処理
    private void OnPhaseStart()
    {
        _canvas.gameObject.SetActive(true);

        _navigateAction.action.performed += OnNavigate;
    }

    //売買フェーズ終了時処理（待機可）
    private async UniTask OnComplete()
    {
        EventSystem.current.SetSelectedGameObject(null);
        _lastSelectedObject = null;

        await _conversationController.OnEnd();

        await _rugController.PlayEndAnimation();

        _canvas.gameObject.SetActive(false);
        _navigateAction.action.performed -= OnNavigate;
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

    public async void OnSetTrader()
    {
        var token = this.GetCancellationTokenOnDestroy();

        InputSystemManager.Instance.ActionDisable();

        SetFirstSelectedGameObject();

        //スタート時の会話を行う
        await _conversationController.OnStart();

        EventSystem.current.SetSelectedGameObject(null);
        _lastSelectedObject = null;

        InputSystemManager.Instance.ActionEnable();

        //キー入力待機
        while (!_any.action.WasPressedThisFrame())
        {
            await UniTask.Yield(token);
        }

        //会話終了後ラグを引く
        await _rugController.PlayStartAnimation();

        //制限時間の開始
        _timeLimitController.Play();

        SetFirstSelectedGameObject();
    }

    private void SetFirstSelectedGameObject()
    {
        EventSystem.current.SetSelectedGameObject(_firstSelectable.gameObject);
        _lastSelectedObject = _firstSelectable.gameObject;
    }
}