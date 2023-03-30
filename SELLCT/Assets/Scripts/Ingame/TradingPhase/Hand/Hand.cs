using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField, Min(0)] int _handCapacity;

    readonly List<Card> _cards = new();
    int _addHandCapacityValue = 0;

    public int Capacity => _handCapacity + _addHandCapacityValue;
    public IReadOnlyList<Card> Cards => _cards;

    public void Add(Card card)
    {
        if (_cards.Count >= Capacity) throw new HandCapacityExceededException();

        _cards.Add(card);
    }

    public void Remove(Card card)
    {
        if (!_cards.Contains(card))
        {
            Debug.LogError("手札に存在しないカードが選択されました。仕様を確認してください。\n" + card);
            return;
        }

        _cards.Remove(card);
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
        var list = _cards.FindAll(c => c.Equals(card));

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