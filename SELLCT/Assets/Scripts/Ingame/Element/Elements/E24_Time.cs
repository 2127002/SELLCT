using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E24_Time : Card
{
    [SerializeField] PhaseController _phaseController = default!;
    [SerializeField] TimeLimitController _timeLimitController = default!;
    [SerializeField] float _addValueInSeconds;
    [SerializeField] float _reduceValueInSeconds;

    [SerializeField] End_1 end_1;
    public override int Id => 24;

    private void Awake()
    {
        _phaseController.OnTradingPhaseStart.Add(OnTradingPhaseStart);
    }

    private void OnDestroy()
    {
        _phaseController.OnTradingPhaseStart.Remove(OnTradingPhaseStart);
    }

    public override void Buy()
    {
        _timeLimitController.AddTimeLimit(_addValueInSeconds, _handMediator.FindAll(this));
    }

    public override void OnPressedU6Button()
    {
        throw new System.NotImplementedException();
    }

    public override void Sell()
    {
        _timeLimitController.ReduceTimeLimit(_reduceValueInSeconds, _handMediator.FindAll(this));
    }

    private void OnTradingPhaseStart()
    {
        _timeLimitController.Generate(_handMediator.FindAll(this));
        GameOverChecker();
    }
    private void GameOverChecker()
    {
        //�������ۂ�Time��1�����Ȃ��Ȃ�Q�[���I�[�o�[
        if (_handMediator.ContainsCard(this)) return;

        Debug.LogWarning(StringManager.ToDisplayString("�����Ȃ��Ȃ�܂����I�V�[��3�ɑJ�ڂ��鏈���͖������Ȃ��ߑ��s����܂��B"));
        end_1.End_1Transition();
    }
}
