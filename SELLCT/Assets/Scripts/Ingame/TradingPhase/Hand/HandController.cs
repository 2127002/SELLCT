using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField, Min(0)] int _handCapacity;

    readonly List<IElements> _hands = new();

    public void Add(IElements card)
    {

        _hands.Add(card);
    }

    public IElements PopBack()
    {
        IElements element = _hands[0];
        _hands.Remove(element);

        return element;
    }
}
