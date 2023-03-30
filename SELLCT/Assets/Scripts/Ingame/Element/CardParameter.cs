using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CardParameter
{
    [Header("購入/売却時の金額")]
    [SerializeField, Min(0)] int _price;
    [SerializeField] string _name;
    [Header("カードのレアリティ。基本的に、数値が高い方がドロー確率が高い")]
    [SerializeField, Min(1)] int _rarity = 1;
    [Header("カード売却時に消滅するか。チェックマークを入れることで商人のデッキに入らず消滅する。")]
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

    public int Rarity()
    {
        return _rarity;
    }

    public bool IsDisposedOfAfterSell()
    {
        return _isDisposedOfAfterSell;
    }
}
