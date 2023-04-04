using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class E2_Sell : Card
{
    [SerializeField] Color _defaultColor = default!;
    [SerializeField] Color _disabledColor = default!;
    [SerializeField] CardUIInstance _handCardUIInstance = default!;

    public override int Id => 2;

    public override void Buy()
    {
        _moneyPossessedController.DecreaseMoney(_parameter.GetMoney());

        OnBuy();
    }

    public override void OnPressedU6Button()
    {
        throw new System.NotImplementedException();
    }

    public override void Sell()
    {
        _moneyPossessedController.IncreaseMoney(_parameter.GetMoney());

        if (_handMediator.ContainsCard(this)) return;

        OnSell();
    }

    public void OnBuy()
    {
        foreach (var cardUIHandler in _handCardUIInstance.Handlers)
        {
            cardUIHandler.EnabledSelectebility();

            foreach (var cardImage in cardUIHandler.CardImages)
            {
                cardImage.color = _defaultColor;
            }
        }
    }

    public void OnSell()
    {
        foreach (var cardUIHandler in _handCardUIInstance.Handlers)
        {
            cardUIHandler.DisableSelectability();

            foreach (var cardImage in cardUIHandler.CardImages)
            {
                cardImage.color = _disabledColor;
            }
        }
    }
}
