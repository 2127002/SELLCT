using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//interfaceとして実装したいですが、そうするとインスペクターで管理できないため
//同じ効果を持つ抽象クラスにします。
public abstract class Card : MonoBehaviour
{
    public abstract bool IsDisposedOfAfterSell { get; }
    public abstract int Rarity { get; }
    public abstract void Buy();
    public abstract void Sell();
    public abstract void Passive();
}