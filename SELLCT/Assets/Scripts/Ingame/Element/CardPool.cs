using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPool : MonoBehaviour
{
    [SerializeField] List<InitCardCount> _cardCapacity;
    Deck _pool = new();

    void Awake()
    {
        foreach (var item in _cardCapacity)
        {
            for (int i = 0; i < item.InitCount; i++)
            {
                _pool.Add(item.Card);
            }
        }
    }
}
