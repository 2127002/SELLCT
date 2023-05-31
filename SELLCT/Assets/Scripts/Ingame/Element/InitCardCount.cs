using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct InitCardCount
{
    [SerializeField] Card card;
    [SerializeField,Min(0)] int initcount;

    public Card Card => card;
    public int InitCount => initcount;
}
