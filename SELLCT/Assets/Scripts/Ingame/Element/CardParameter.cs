using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CardParameter
{
    [Header("�w��/���p���̋��z")]
    [SerializeField, Min(0)] int _price;
    [SerializeField] string _name;
    [Header("�J�[�h�̃��A���e�B�B��{�I�ɁA���l�����������h���[�m��������")]
    [SerializeField, Min(1)] int _rarity = 1;
    [Header("�J�[�h���p���ɏ��ł��邩�B�`�F�b�N�}�[�N�����邱�Ƃŏ��l�̃f�b�L�ɓ��炸���ł���B")]
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
