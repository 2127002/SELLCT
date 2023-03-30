using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DeckMediator : MonoBehaviour
{
    public abstract void DrawCard();
    public abstract void RemoveHandCard(Card card);
    public abstract void AddDeck(Card card);
    public abstract void UpdeteCardSprites();
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
    /// <summary>
    /// CardUIHandlerに応じた手札のカードを取得する
    /// </summary>
    /// <param name="handler">カードの情報を取得したいCardUIHandler</param>
    /// <returns>取得したカード</returns>
    public abstract Card GetCardAtCardUIHandler(CardUIHandler handler);
}
