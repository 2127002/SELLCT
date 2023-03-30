using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class E7_Move : Card
{
    [SerializeField] Selectable _selectable;
    [SerializeField] Image _u7 = default!;

    public override int Id => 7;

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
        MoveChecker();
    }

    public void MoveChecker()
    {
        if (_handMediator.ContainsCard(this)) return;
        _u7.enabled = false;
        _selectable.interactable = false;
    }
}
