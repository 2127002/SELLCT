using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICard
{
    void AddCardToDeck();
    void Buy();
    void Sell();
    void Passive();
}