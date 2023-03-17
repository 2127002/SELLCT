using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DeckMediator : MonoBehaviour
{
    public abstract Card TakeDeckCard();
    public abstract void RemoveHandCard(Card card);
    public abstract void AddDeck(Card card);
    public abstract void RearrangeCardSlots();
    public abstract void AddBuyingDeck(Card card);
    /// <summary>
    /// 山札か手札、購入直後デッキに指定カードが存在するか返す
    /// </summary>
    /// <param name="card">判定したいカード</param>
    /// <returns>true:あった</returns>
    public abstract bool ContainsCard(Card card);
    /// <summary>
    /// 山札か手札、購入直後デッキに指定カードが何枚存在するか返す
    /// </summary>
    /// <param name="card">判定したいカード</param>
    /// <returns>存在した枚数</returns>
    public abstract int FindAll(Card card);
}
