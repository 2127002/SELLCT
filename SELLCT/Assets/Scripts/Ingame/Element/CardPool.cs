using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲーム内に登場するカード一覧を定義する。
/// </summary>
public class CardPool : MonoBehaviour
{
    [SerializeField] List<InitCardCount> _cardCapacity;
    readonly List<Card> _cardPool = new();

    //Edit > Project Settings > Script Execution Orderで実行順を調整しています。
    void Awake()
    {
        //初期化
        foreach (var item in _cardCapacity)
        {
            for (int i = 0; i < item.InitCount; i++)
            {
                _cardPool.Add(item.Card);
            }
        }
    }

    /// <summary>
    /// 指定のカードを取り出す処理
    /// </summary>
    /// <param name="card">取り出したいカード</param>
    /// <returns></returns>
    public Card Draw(Card card)
    {
        if (!_cardPool.Remove(card)) return EEX_null.Instance;

        return card;
    }

    /// <summary>
    /// 先頭から1枚ドローする処理
    /// </summary>
    /// <returns></returns>
    public Card Draw()
    {
        if (_cardPool.Count == 0) return EEX_null.Instance;

        Card card = _cardPool[0];
        _cardPool.RemoveAt(0);

        return card;
    }

    public IReadOnlyList<Card> Pool()
    {
        return _cardPool;
    }
}
