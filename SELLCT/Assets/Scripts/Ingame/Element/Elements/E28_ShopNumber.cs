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

        //���ɓo�^���Ă������U��������
        _phaseController.OnTradingPhaseStart.Remove(AddHandCapacity);

        //����̔����t�F�[�Y���甽�f������B���s���Ńh���[������ɂ��������ߐ擪�ɂ���B
        //�擪�ɂ��邱�Ƃɂ���肪����������A�h���[����ɂȂ�悤�ɍH�ʂ��Ă��������B
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
        //���݂̔����t�F�[�Y�ł��̃G�������g�𔃂�����������
        _traderHand.AddHandCapacity(_currentPhaseBuyingCount);

        //��x���s������o�^��������
        _phaseController.OnTradingPhaseStart.Remove(AddHandCapacity);
        _currentPhaseBuyingCount = 0;
    }
}
