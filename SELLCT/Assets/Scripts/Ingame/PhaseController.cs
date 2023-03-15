using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;

public class PhaseController : MonoBehaviour
{
    enum Phase
    {
        None,
        Exploration,
        Trading
    }

    Phase _currentPhase = Phase.None;

    public event Action onGameStart;
    public event Action onExplorationPhaseStart;
    public event Action onTradingPhaseStart;
    public event Action onExplorationPhaseComplete;
    public List<Func<UniTask>> onTradingPhaseComplete = new();

    //イベントの登録が終わってから実行したいため、Startで行う。
    private void Start()
    {
        //ゲーム開始時処理
        onGameStart?.Invoke();

        //初期フェーズは探索フェーズ
        StartExplorationPhase();
    }

    private void OnDestroy()
    {
        //リスナーの解除
        onExplorationPhaseStart = null;
        onTradingPhaseStart = null;
        onExplorationPhaseComplete = null;
        onTradingPhaseComplete.Clear();
    }

    private void StartExplorationPhase()
    {
        //すでに探索フェーズなら実行しない。
        if (_currentPhase == Phase.Exploration) return;

        _currentPhase = Phase.Exploration;
        onExplorationPhaseStart?.Invoke();
    }
    private void StartTradingPhase()
    {
        //すでに売買フェーズなら実行しない。
        if (_currentPhase == Phase.Trading) return;

        _currentPhase = Phase.Trading;
        onTradingPhaseStart?.Invoke();
    }

    public void CompleteExplorationPhase()
    {
        //探索フェーズでないなら実行しない。
        if (_currentPhase != Phase.Exploration) return;

        onExplorationPhaseComplete?.Invoke();

        //遷移先に遷移
        StartTradingPhase();
    }
    public async void CompleteTradingPhase()
    {
        //売買フェーズでないなら実行しない。
        if (_currentPhase != Phase.Trading) return;

        //並列で待機
        await UniTask.WhenAll(Array.ConvertAll(onTradingPhaseComplete.ToArray(), unitask => unitask.Invoke()));

        //遷移先に遷移
        StartExplorationPhase();
    }
}