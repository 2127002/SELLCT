using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [Header("手札の限度枚数です。\n-1にすると無限になります。")]
    [SerializeField, Min(-1)] int _handCapacity;

    readonly List<Card> _cards = new();
    int _addHandCapacityValue = 0;

    //無限と言えど高々カード枚数は30枚程度。増えたらここの値を増やしてください。
    public int Capacity => (_handCapacity == -1 ? 30 : _handCapacity) + _addHandCapacityValue;
    public IReadOnlyList<Card> Cards => _cards;

    public void Add(Card card)
    {
        if (_cards.Count >= Capacity) throw new HandCapacityExceededException();

        _cards.Add(card);
    }

    public bool Remove(Card card)
    {
        return _cards.Remove(card);
    }

    public int CalcDrawableCount()
    {
        return Capacity - _cards.Count;
    }

    public bool ContainsCard(Card card)
    {
        return _cards.Contains(card);
    }

    public int FindAll(Card card)
    {
        var list = _cards.FindAll(c => c.Id.Equals(card.Id));

        return list.Count;
    }

    public void AddHandCapacity(int amount)
    {
        _addHandCapacityValue += amount;

        if (_addHandCapacityValue < 0) throw new System.ArgumentOutOfRangeException();
    }

    public void SetDefaultHandCapacity(int newCapacity)
    {
        _handCapacity = newCapacity;
    }
}

public class HandCapacityExceededException : System.Exception
{
    public HandCapacityExceededException()
    {
        Debug.LogError("手札のキャパシティを超えて追加しようとしました。追加はキャンセルされました。");
    }
}