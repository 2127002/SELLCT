using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E28_ShopNumber : Card
{
    [SerializeField] Hand _traderHand = default!;
    [SerializeField] PhaseController _phaseController = default!;

    public override int Id => 28;

    int _currentPhaseBuyingCount = 0;

    private void Awake()
    {
        _phaseController.OnGameStart.Add(OnGameStart);
    }

    private void OnDestroy()
    {
        _phaseController.OnGameStart.Remove(OnGameStart);
    }

    private void OnGameStart()
    {
        int add = _handMediator.FindAll(this);
        _traderHand.AddHandCapacity(add);

        for (int i = 0; i < add; i++)
        {
            _handMediator.DrawCard();
        }
    }

    public override void Buy()
    {
        base.Buy();

        _currentPhaseBuyingCount++;

        //既に登録していたら一旦解除する
        _phaseController.OnTradingPhaseStart.Remove(AddHandCapacity);

        //次回の売買フェーズから反映させる。実行順でドローよりも先にしたいため先頭にする。
        //先頭にすることにより問題が発生したら、ドローより先になるように工面してください。
        _phaseController.OnTradingPhaseStart.Insert(0, AddHandCapacity);
    }

    public override void OnPressedU6Button()
    {
        throw new System.NotImplementedException();
    }

    public override void Sell()
    {
        base.Sell();

        _traderHand.AddHandCapacity(-1);
    }

    private void AddHandCapacity()
    {
        //現在の売買フェーズでこのエレメントを買った数分足す
        _traderHand.AddHandCapacity(_currentPhaseBuyingCount);

        //一度実行したら登録解除する
        _phaseController.OnTradingPhaseStart.Remove(AddHandCapacity);
        _currentPhaseBuyingCount = 0;
    }
}
