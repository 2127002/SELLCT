using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class E2_Sell : Card
{
    [SerializeField] Color defaultColor = default!;
    [SerializeField] Color changeColor = default!;
    [SerializeField] CardUIInstance cardUIInstance = default!;

    public override void Buy()
    {
        _controller.DecreaseMoney(_parameter.GetMoney());

        BuyChecker();
    }

    public override void Passive()
    {
        throw new System.NotImplementedException();
    }

    public override void Sell()
    {
        _controller.IncreaseMoney(_parameter.GetMoney());

        if (_handMediator.ContainsCard(this)) return;

        SellChecker();
    }
    public void BuyChecker()
    {
        foreach (var cardUIHandler in cardUIInstance.Handlers)
        {
            cardUIHandler.EnabledSelectebility();

            foreach (var cardImage in cardUIHandler.CardImages)
            {
                cardImage.color = defaultColor;
            }
        }
    }
    public void SellChecker()
    {
        foreach (var cardUIHandler in cardUIInstance.Handlers)
        {
            cardUIHandler.DisableSelectability();

            foreach (var cardImage in cardUIHandler.CardImages)
            {
                cardImage.color = changeColor;
            }
        }
    }
}
