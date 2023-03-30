using UnityEngine;

public class E30_Name : Card
{
    [SerializeField] TraderController _traderController = default!;

    public override int Id => 30;

    public override void Buy()
    {
        _moneyPossessedController.DecreaseMoney(_parameter.GetMoney());
    }

    public override void OnPressedU6Button()
    {
        throw new System.NotImplementedException();
    }

    public override void Sell()
    {
        _moneyPossessedController.IncreaseMoney(_parameter.GetMoney());

        //�c���Name�J�[�h�������Ă��Ȃ��Ă��g���[�_�[�ɖ��O��t����
        _traderController.CurrentTrader.Name = _parameter.GetName();
    }
}