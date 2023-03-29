using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E30_Name : Card
{
    [SerializeField] TraderController _traderController = default!;

    public override void Buy()
    {
        _controller.DecreaseMoney(_parameter.GetMoney());
    }

    public override void Passive()
    {
        throw new System.NotImplementedException();
    }

    public override void Sell()
    {
        _controller.IncreaseMoney(_parameter.GetMoney());

        //�c���Name�J�[�h�������Ă��Ȃ��Ă��g���[�_�[�ɖ��O��t����
        _traderController.CurrentTrader.Name = _parameter.GetName();
    }
}