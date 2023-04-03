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

    //����ȂǂɎg��Action
    [SerializeField] InputActionReference _submitAction = default!;
    [SerializeField] InputActionReference _clickAction = default!;

    Phase _currentPhase = Phase.None;

    //�Ӑ}�I�ɃA�N�V�����̎��s�������ւ��������߁AList�ɂ��Ă��܂��B
    public readonly List<Action> OnGameStart = new();
    public event Action OnExplorationPhaseStart;

    //�Ӑ}�I�ɃA�N�V�����̎��s�������ւ��������߁AList�ɂ��Ă��܂��B
    public readonly List<Action> OnTradingPhaseStart = new();
    public event Action OnExplorationPhaseComplete;
    public readonly List<Func<UniTask>> OnTradingPhaseComplete = new();

    //�C�x���g�̓o�^���I����Ă�����s���������߁AStart�ōs���B
    private void Start()
    {
        //�Q�[���J�n������
        for (int i = 0; i < OnGameStart.Count; i++)
        {
            OnGameStart[i]?.Invoke();
        }

        //�����t�F�[�Y�͒T���t�F�[�Y
        StartExplorationPhase();
    }

    private void OnDestroy()
    {
        //���X�i�[�̉���
        OnGameStart.Clear();
        OnExplorationPhaseStart = null;
        OnTradingPhaseStart.Clear();
        OnExplorationPhaseComplete = null;
        OnTradingPhaseComplete.Clear();
    }

    private void StartExplorationPhase()
    {
        //���łɒT���t�F�[�Y�Ȃ���s���Ȃ��B
        if (_currentPhase == Phase.Exploration) return;

        _currentPhase = Phase.Exploration;
        OnExplorationPhaseStart?.Invoke();
    }

    private void StartTradingPhase()
    {
        //���łɔ����t�F�[�Y�Ȃ���s���Ȃ��B
        if (_currentPhase == Phase.Trading) return;

        _currentPhase = Phase.Trading;

        for (int i = 0; i < OnTradingPhaseStart.Count; i++)
        {
            OnTradingPhaseStart[i]?.Invoke();
        }
    }

    public void CompleteExplorationPhase()
    {
        //�T���t�F�[�Y�łȂ��Ȃ���s���Ȃ��B
        if (_currentPhase != Phase.Exploration) return;

        OnExplorationPhaseComplete?.Invoke();

        //�J�ڐ�ɑJ��
        StartTradingPhase();
    }

    public async void CompleteTradingPhase()
    {
        //�����t�F�[�Y�łȂ��Ȃ���s���Ȃ��B
        if (_currentPhase != Phase.Trading) return;

        DisableSubmitAction();

        //����őҋ@
        await UniTask.WhenAll(Array.ConvertAll(OnTradingPhaseComplete.ToArray(), unitask => unitask.Invoke()));

        EnableSubmitAction();

        //�J�ڐ�ɑJ��
        StartExplorationPhase();
    }

    private async void DisableSubmitAction()
    {
        //�A�N�V�����̃L�����Z����������Ȃ��Ȃ邽�߁A1�t���[���ҋ@����
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