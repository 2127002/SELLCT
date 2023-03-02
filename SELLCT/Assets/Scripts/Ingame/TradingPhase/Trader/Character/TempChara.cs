using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempChara : MonoBehaviour,ITrader
{
    public string BuyMessage(ICard card)
    {
        throw new System.NotImplementedException();
    }

    public string CardMessage(ICard card)
    {
        throw new System.NotImplementedException();
    }

    public string EndMessage()
    {
        return "Seriously, they should have just prepared a Japanese font. But I get it, it can be a hassle.";
    }

    public string SellMessage(ICard card)
    {
        throw new System.NotImplementedException();
    }

    public string StartMessage()
    {
        return "Hi! Wondering why this message is in English? Well, it's because the font doesn't support Japanese characters!";
    }
}
