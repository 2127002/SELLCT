using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct FavoriteCards
{
    [Header("���C�ɓ���G�������g�ꗗ�ł��B")]
    [SerializeField] List<Card> _favoriteCards;
    [Header("��L�̃G�������g�̍ہA�ǉ��ő�������D���x�̒l�ł��B")]
    [SerializeField, Min(0)] int _bonus;

    public IReadOnlyList<Card> FavoriteCardsList => _favoriteCards;
    public int Bonus => _bonus;
}
