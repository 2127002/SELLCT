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

    [Header("�ő�l�Ə����l�iE24�̏�����*���̒l�j�̌v�Z�Ɏg�p���܂��B")]
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
        //��ɃX�e�[�g�m�F������
        if (_state != State.Playing) return;
        if (_timeLimit == null) throw new ArgumentNullException("�������Ԃ���������Ă��܂���B");

        _timeLimit.DecreaseTimeDeltaTime();

        float maxTimeLimit = _currentE24Count * _timeLimitRate;

        //���v��i�߂�
        _timeLimitView.Rotate(maxTimeLimit, _timeLimit.CurrentTimeLimitValue);
        _timeLimitView.Scale(maxTimeLimit, _timeLimit.CurrentTimeLimitValue);
    }

    private void TimeLimitChecker()
    {
        if (!_timeLimit.IsTimeLimitReached()) return;

        Debug.Log("�������Ԃł�");

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

        //�j�𒲐�
        AdjustView(value);
    }

    public void ReduceTimeLimit(float value, int currentE24Count)
    {        
        _currentE24Count = currentE24Count;

        _timeLimit = _timeLimit.ReduceTimeLimit(new(value, _timeLimitRate), currentE24Count);

        //�j�𒲐�
        AdjustView(value);
    }

    //���v�̐j�𒲐�����
    private void AdjustView(float value)
    {
        float maxTimeLimit = _currentE24Count * _timeLimitRate;
        _timeLimitView.Rotate(maxTimeLimit, _timeLimit.CurrentTimeLimitValue);
        _timeLimitView.Scale(maxTimeLimit, _timeLimit.CurrentTimeLimitValue);
    }

    /// <summary>
    /// �͂��߂��琧�����Ԃ��J�n����B
    /// </summary>
    /// <param name="forced">�ꎞ��~���ł��Đ����邩</param>
    public void Play(bool forced = false)
    {
        //�������s�łȂ�
        if (!forced)
        {
            //�|�[�Y���Ȃ�ĊJ���Ȃ�
            if (_state == State.Paused) return;
        }

        _state = State.Playing;
        _timeLimit = new(_currentE24Count * _timeLimitRate, _timeLimitRate);
    }

    /// <summary>
    /// �������Ԃ̌������ĊJ����B
    /// </summary>
    public void Resume()
    {
        if (_state != State.Paused)
        {
            Debug.LogWarning("�|�[�Y���ȊO�͍ĊJ�R�}���h�����s���邱�Ƃ͏o���܂���");
            return;
        }

        _state = State.Playing;
    }

    /// <summary>
    /// �������Ԃ��ꎞ��~����B�ĊJ����ۂ�Resume()���Ă�ł��������B
    /// </summary>
    public void Paused()
    {
        //�v���C���ȊO��Pause�ł��Ȃ�
        if (_state != State.Playing)
        {
            Debug.LogWarning("�������ԍĐ����ȊO�͈ꎞ��~�ł��܂���");
            return;
        }

        _state = State.Paused;
    }

    /// <summary>
    /// �������Ԃ����S�Ɏ~�߂�B�I�������͍s���Ȃ����Ƃɒ��ӂ��Ă��������B
    /// </summary>
    public void Stop()
    {
        _state = State.Stopped;
        _timeLimit = null;
    }
}
