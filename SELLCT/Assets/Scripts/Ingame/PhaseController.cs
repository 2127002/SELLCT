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

    //�C�x���g�̓o�^���I����Ă�����s���������߁AStart�ōs���B
    private void Start()
    {
        //�Q�[���J�n������
        onGameStart?.Invoke();

        //�����t�F�[�Y�͒T���t�F�[�Y
        StartExplorationPhase();
    }

    private void OnDestroy()
    {
        //���X�i�[�̉���
        onExplorationPhaseStart = null;
        onTradingPhaseStart = null;
        onExplorationPhaseComplete = null;
        onTradingPhaseComplete.Clear();
    }

    private void StartExplorationPhase()
    {
        //���łɒT���t�F�[�Y�Ȃ���s���Ȃ��B
        if (_currentPhase == Phase.Exploration) return;

        _currentPhase = Phase.Exploration;
        onExplorationPhaseStart?.Invoke();
    }
    private void StartTradingPhase()
    {
        //���łɔ����t�F�[�Y�Ȃ���s���Ȃ��B
        if (_currentPhase == Phase.Trading) return;

        _currentPhase = Phase.Trading;
        onTradingPhaseStart?.Invoke();
    }

    public void CompleteExplorationPhase()
    {
        //�T���t�F�[�Y�łȂ��Ȃ���s���Ȃ��B
        if (_currentPhase != Phase.Exploration) return;

        onExplorationPhaseComplete?.Invoke();

        //�J�ڐ�ɑJ��
        StartTradingPhase();
    }
    public async void CompleteTradingPhase()
    {
        //�����t�F�[�Y�łȂ��Ȃ���s���Ȃ��B
        if (_currentPhase != Phase.Trading) return;

        //����őҋ@
        await UniTask.WhenAll(Array.ConvertAll(onTradingPhaseComplete.ToArray(), unitask => unitask.Invoke()));

        //�J�ڐ�ɑJ��
        StartExplorationPhase();
    }
}