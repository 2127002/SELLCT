using UnityEngine;

public class E30_Name : Card
{
    [SerializeField] TraderController _traderController = default!;

    public override int Id => 30;

    public override void Buy()
    {
        base.Buy();

    }

    public override void OnPressedU6Button()
    {
        throw new System.NotImplementedException();
    }

    public override void Sell()
    {
        base.Sell();

        _traderController.CurrentTrader.Name = _parameter.GetName();
    }
}
