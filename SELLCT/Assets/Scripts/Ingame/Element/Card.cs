using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//interface�Ƃ��Ď����������ł����A��������ƃC���X�y�N�^�[�ŊǗ��ł��Ȃ�����
//�������ʂ������ۃN���X�ɂ��܂��B
public abstract class Card : MonoBehaviour
{
    public abstract bool IsDisposedOfAfterSell { get; }
    public abstract int Rarity { get; }
    public abstract void Buy();
    public abstract void Sell();
    public abstract void Passive();
}