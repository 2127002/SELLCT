using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDeck
{
    void Add(Card card);
    Card Draw();
    public bool ContainsCard(Card card);
    public int FindAll(Card card);
}
