using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class E1_Buy : Card
{
    [SerializeField] Color changeColor = default!;
    [SerializeField] CardUIInstance cardUIInstance = default!;

    public override void Buy()
    {
        _controller.DecreaseMoney(_parameter.GetMoney());

    }

    public override void Passive()
    {
        //TODO:SE2
    }

    public override void Sell()
    {
        _controller.IncreaseMoney(_parameter.GetMoney());
        BuyChecker();
    }
    private void BuyChecker()
    {
        if (_handMediator.ContainsCard(this)) return;

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
