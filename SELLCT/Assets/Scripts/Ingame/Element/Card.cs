using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//interfaceとして実装したいですが、そうするとインスペクターで管理できないため
//同じ効果を持つ抽象クラスにします。
public abstract class Card : MonoBehaviour
{
    public abstract IReadOnlyList<Sprite> CardSprite { get; }
    public abstract bool IsDisposedOfAfterSell { get; }
    public abstract int Rarity { get; }
    public abstract bool ContainsPlayerDeck { get; }
    public abstract void Buy();
    public abstract void Sell();
    /// <summary>
    /// 探索フェーズにおけるU6ボタン押下時の効果
    /// </summary>
    public abstract void Passive();
}