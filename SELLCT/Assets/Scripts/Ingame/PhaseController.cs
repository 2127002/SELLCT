using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine.InputSystem;

public class PhaseController : MonoBehaviour
{
    enum Phase
    {
        None,
        Exploration,
        Trading
    }

    //決定などに使うAction
    [SerializeField] InputActionReference _submitAction = default!;
    [SerializeField] InputActionReference _clickAction = default!;

    Phase _currentPhase = Phase.None;

    //意図的にアクションの実行順を入れ替えたいため、Listにしています。
    public readonly List<Action> OnGameStart = new();
    public event Action OnExplorationPhaseStart;

    //意図的にアクションの実行順を入れ替えたいため、Listにしています。
    public readonly List<Action> OnTradingPhaseStart = new();
    public event Action OnExplorationPhaseComplete;
    public readonly List<Func<UniTask>> OnTradingPhaseComplete = new();

    //イベントの登録が終わってから実行したいため、Startで行う。
    private void Start()
    {
        //ゲーム開始時処理
        for (int i = 0; i < OnGameStart.Count; i++)
        {
            OnGameStart[i]?.Invoke();
        }

        //初期フェーズは探索フェーズ
        StartExplorationPhase();
    }

    private void OnDestroy()
    {
        //リスナーの解除
        OnGameStart.Clear();
        OnExplorationPhaseStart = null;
        OnTradingPhaseStart.Clear();
        OnExplorationPhaseComplete = null;
        OnTradingPhaseComplete.Clear();
    }

    private void StartExplorationPhase()
    {
        //すでに探索フェーズなら実行しない。
        if (_currentPhase == Phase.Exploration) return;

        _currentPhase = Phase.Exploration;
        OnExplorationPhaseStart?.Invoke();
    }

    private void StartTradingPhase()
    {
        //すでに売買フェーズなら実行しない。
        if (_currentPhase == Phase.Trading) return;

        _currentPhase = Phase.Trading;

        for (int i = 0; i < OnTradingPhaseStart.Count; i++)
        {
            OnTradingPhaseStart[i]?.Invoke();
        }
    }

    public void CompleteExplorationPhase()
    {
        //探索フェーズでないなら実行しない。
        if (_currentPhase != Phase.Exploration) return;

        OnExplorationPhaseComplete?.Invoke();

        //遷移先に遷移
        StartTradingPhase();
    }

    public async void CompleteTradingPhase()
    {
        //売買フェーズでないなら実行しない。
        if (_currentPhase != Phase.Trading) return;

        DisableSubmitAction();

        //並列で待機
        await UniTask.WhenAll(Array.ConvertAll(OnTradingPhaseComplete.ToArray(), unitask => unitask.Invoke()));

        EnableSubmitAction();

        //遷移先に遷移
        StartExplorationPhase();
    }

    private async void DisableSubmitAction()
    {
        //アクションのキャンセルが見つからなくなるため、1フレーム待機する
        await UniTask.Yield();

        _submitAction.action.Disable();
        _clickAction.action.Disable();
    }

    private void EnableSubmitAction()
    {
        _submitAction.action.Enable();
        _clickAction.action.Enable();
    }
}