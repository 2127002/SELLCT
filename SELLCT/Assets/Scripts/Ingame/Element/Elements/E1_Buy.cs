using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class E1_Buy : Card
{
    [SerializeField] Color _disabledColor = default!;
    [SerializeField] CardUIInstance _goodsCardUIInstance = default!;

    public override int Id => 1;

    public override void Buy()
    {
        _moneyPossessedCcontroller.DecreaseMoney(_parameter.GetMoney());
    }

    public override void OnPressedU6Button()
    {
        throw new System.NotImplementedException();
    }

    public override void Sell()
    {
        _moneyPossessedCcontroller.IncreaseMoney(_parameter.GetMoney());
        OnSell();
    }

    private void OnSell()
    {
        if (_handMediator.ContainsCard(this)) return;

        foreach (var cardUIHandler in _goodsCardUIInstance.Handlers)
        {
            cardUIHandler.DisableSelectability();

            foreach (var cardImage in cardUIHandler.CardImages)
            {
                cardImage.color = _disabledColor;
            }
        }
    }
}
