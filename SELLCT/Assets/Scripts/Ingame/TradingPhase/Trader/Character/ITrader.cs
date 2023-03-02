using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITrader
{
    string StartMessage();
    string EndMessage();
    string CardMessage(ICard card);
    string BuyMessage(ICard card);
    string SellMessage(ICard card);
}
