using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//interface�Ƃ��Ď����������ł����A��������ƃV���A���C�Y�o���Ȃ�����
//�������ʂ������ۃN���X�ɂ��܂��B
public abstract class Card : MonoBehaviour
{
    public abstract void AddCardToDeck();
    public abstract void Buy();
    public abstract void Sell();
    public abstract void Passive();
}