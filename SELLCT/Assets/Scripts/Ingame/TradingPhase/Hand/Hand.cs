using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField, Min(0)] int _handCapacity;

    readonly List<Card> _cards = new();

    public void Add(Card card)
    {
        if(_cards.Count >= _handCapacity)throw new HandCapacityExceededException();

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
        return _handCapacity - _cards.Count;
    }
}

public class HandCapacityExceededException : System.Exception
{
    public HandCapacityExceededException()
    {
        Debug.LogError("手札のキャパシティを超えて追加しようとしました。追加はキャンセルされました。");
    }
}