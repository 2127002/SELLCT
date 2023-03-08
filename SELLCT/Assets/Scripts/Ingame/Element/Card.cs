using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//interfaceとして実装したいですが、そうするとシリアライズ出来ないため
//同じ効果を持つ抽象クラスにします。
public abstract class Card : MonoBehaviour
{
    public abstract void AddCardToDeck();
    public abstract void Buy();
    public abstract void Sell();
    public abstract void Passive();
}