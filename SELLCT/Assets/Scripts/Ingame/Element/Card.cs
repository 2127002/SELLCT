using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//interface�Ƃ��Ď����������ł����A��������ƃC���X�y�N�^�[�ŊǗ��ł��Ȃ�����
//�������ʂ������ۃN���X�ɂ��܂��B
public abstract class Card : MonoBehaviour
{
    public abstract IReadOnlyList<Sprite> CardSprite { get; }
    public abstract bool IsDisposedOfAfterSell { get; }
    public abstract int Rarity { get; }
    public abstract bool ContainsPlayerDeck { get; }
    public abstract void Buy();
    public abstract void Sell();
    /// <summary>
    /// �T���t�F�[�Y�ɂ�����U6�{�^���������̌���
    /// </summary>
    public abstract void Passive();
}