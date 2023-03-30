using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct FavoriteCards
{
    [Header("お気に入りエレメント一覧です。")]
    [SerializeField] List<Card> _favoriteCards;
    [Header("上記のエレメントの際、追加で増加する好感度の値です。")]
    [SerializeField, Min(0)] int _bonus;

    public IReadOnlyList<Card> FavoriteCardsList => _favoriteCards;
    public int Bonus => _bonus;
}
