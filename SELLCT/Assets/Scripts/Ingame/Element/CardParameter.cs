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

    [SerializeField, Min(1)] int _rarity = 1;
    [SerializeField] bool _isDisposedOfAfterSell;

    Money _money;

    public Money GetMoney()
    {
        if (_money == null) _money = new(_price);

        return _money;
    }

    public string GetName()
    {
        if (string.IsNullOrEmpty(_name)) return "No Name";

        return _name;
    }    
    
    public void SetName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            _name = "No Name";
            return;
        }

        _name = name;
    }

    public string GetText()
    {
        return _text;
    }

    public int Rarity()
    {
        return _rarity;
    }

    public bool IsDisposedOfAfterSell()
    {
        return _isDisposedOfAfterSell;
    }
}
