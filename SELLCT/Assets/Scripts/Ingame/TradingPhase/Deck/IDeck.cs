using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDeck
{
    void Add(Card card);
    Card Draw();
}
