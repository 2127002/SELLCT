using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField, Min(0)] int _handCapacity;

    readonly List<Card> _cards = new();

    public void Add(Card card)
    {
        if(_cards.Count >= _handCapacity)throw new HandCapacityExceededException();

        _cards.Add(card);
    }

    public void Remove(Card card)
    {
        if (!_cards.Contains(card))
        {
            Debug.LogError("��D�ɑ��݂��Ȃ��J�[�h���I������܂����B�d�l���m�F���Ă��������B\n" + card);
            return;
        }

        _cards.Remove(card);
    }

    public int CalcDrawableCount()
    {
        return _handCapacity - _cards.Count;
    }
}

public class HandCapacityExceededException : System.Exception
{
    public HandCapacityExceededException()
    {
        Debug.LogError("��D�̃L���p�V�e�B�𒴂��Ēǉ����悤�Ƃ��܂����B�ǉ��̓L�����Z������܂����B");
    }
}