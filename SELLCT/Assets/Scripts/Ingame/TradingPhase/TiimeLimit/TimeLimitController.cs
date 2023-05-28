using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class TimeLimitController : MonoBehaviour
{
    enum State
    {
        Playing,
        Paused,
        Stopped,
    }

    [Header("最大値と初期値（E24の所持数*この値）の計算に使用します。")]
    [SerializeField, Min(0)] float _timeLimitRate;

    [SerializeField] PhaseController _phaseController = default!;
    [SerializeField] TimeLimitView _timeLimitView = default!;

    TimeLimit _timeLimit;
    int _currentE24Count;

    State _state = State.Stopped;

    public event Action OnTimeLimit;

    private void Awake()
    {
        _phaseController.OnTradingPhaseComplete.Add(OnPhaseComplete);
    }

    private void OnDestroy()
    {
        _phaseController.OnTradingPhaseComplete.Remove(OnPhaseComplete);
    }

    private void Reset()
    {
        _phaseController = FindObjectOfType<PhaseController>();
    }

    private async UniTask OnPhaseComplete()
    {
        var token = this.GetCancellationTokenOnDestroy();

        Stop();

        await UniTask.Yield(token);
    }

    private void Update()
    {
        Reduce();
    }

    private void FixedUpdate()
    {
        if (_timeLimit != null) TimeLimitChecker();
    }

    private void Reduce()
    {
        //先にステート確認をする
        if (_state != State.Playing) return;
        if (_timeLimit == null) throw new ArgumentNullException("制限時間が生成されていません。");

        _timeLimit.DecreaseTimeDeltaTime();

        float maxTimeLimit = _currentE24Count * _timeLimitRate;

        //時計を進める
        _timeLimitView.Rotate(maxTimeLimit, _timeLimit.CurrentTimeLimitValue);
        _timeLimitView.Scale(maxTimeLimit, _timeLimit.CurrentTimeLimitValue);
    }

    private void TimeLimitChecker()
    {
        if (!_timeLimit.IsTimeLimitReached()) return;

        Debug.Log("制限時間です");

        OnTimeLimit?.Invoke();

        Stop();
    }

    public void SetE24Count(int currentE24Count)
    {
        _currentE24Count = currentE24Count;
    }

    public void AddTimeLimit(float value, int currentE24Count)
    {
        _currentE24Count = currentE24Count;

        _timeLimit = _timeLimit.AddTimeLimit(new(value, _timeLimitRate), currentE24Count);

        //針を調整
        AdjustView(value);
    }

    public void ReduceTimeLimit(float value, int currentE24Count)
    {        
        _currentE24Count = currentE24Count;

        _timeLimit = _timeLimit.ReduceTimeLimit(new(value, _timeLimitRate), currentE24Count);

        //針を調整
        AdjustView(value);
    }

    //時計の針を調整する
    private void AdjustView(float value)
    {
        float maxTimeLimit = _currentE24Count * _timeLimitRate;
        _timeLimitView.Rotate(maxTimeLimit, _timeLimit.CurrentTimeLimitValue);
        _timeLimitView.Scale(maxTimeLimit, _timeLimit.CurrentTimeLimitValue);
    }

    /// <summary>
    /// はじめから制限時間を開始する。
    /// </summary>
    /// <param name="forced">一時停止中でも再生するか</param>
    public void Play(bool forced = false)
    {
        //強制実行でなく
        if (!forced)
        {
            //ポーズ中なら再開しない
            if (_state == State.Paused) return;
        }

        _state = State.Playing;
        _timeLimit = new(_currentE24Count * _timeLimitRate, _timeLimitRate);
    }

    /// <summary>
    /// 制限時間の減少を再開する。
    /// </summary>
    public void Resume()
    {
        if (_state != State.Paused)
        {
            Debug.LogWarning("ポーズ中以外は再開コマンドを実行することは出来ません");
            return;
        }

        _state = State.Playing;
    }

    /// <summary>
    /// 制限時間を一時停止する。再開する際はResume()を呼んでください。
    /// </summary>
    public void Paused()
    {
        //プレイ中以外はPauseできない
        if (_state != State.Playing)
        {
            Debug.LogWarning("制限時間再生中以外は一時停止できません");
            return;
        }

        _state = State.Paused;
    }

    /// <summary>
    /// 制限時間を完全に止める。終了処理は行われないことに注意してください。
    /// </summary>
    public void Stop()
    {
        _state = State.Stopped;
        _timeLimit = null;
    }
}
