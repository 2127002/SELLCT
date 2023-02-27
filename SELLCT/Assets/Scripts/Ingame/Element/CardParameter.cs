using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CardParameter
{
    [SerializeField, Min(0)] int _price;
    [SerializeField] string _name;
    [SerializeField] string _text;
    [SerializeField, Min(0)] int _initialCardCount;

    Money _money;

    public Money GetMoney()
    {
        if (_money == null) _money = new(_price);

        return _money;
    }

    public string GetName()
    {
        return _name;
    }

    public string GetText()
    {
        return _text;
    }
}
