using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [Header("��D�̌��x�����ł��B\n-1�ɂ���Ɩ����ɂȂ�܂��B")]
    [SerializeField, Min(-1)] int _handCapacity;

    readonly List<Card> _cards = new();
    int _addHandCapacityValue = 0;

    //�����ƌ����Ǎ��X�J�[�h������30�����x�B�������炱���̒l�𑝂₵�Ă��������B
    public int Capacity => (_handCapacity == -1 ? 30 : _handCapacity) + _addHandCapacityValue;
    public IReadOnlyList<Card> Cards => _cards;

    public void Add(Card card)
    {
        if (_cards.Count >= Capacity) throw new HandCapacityExceededException();

        foreach (var c in _cards)
        {
            if (c.Id != card.Id) continue;

            c.Count++;
            return;
        }

        _cards.Add(card);
    }

    public bool Remove(Card card)
    {
        if (!_cards.Contains(card))
        {
            Debug.LogError("��D�ɑ��݂��Ȃ��J�[�h���I������܂����B�d�l���m�F���Ă��������B\n" + card);
            return false;
        }

        //�J�[�h�̏������������炷�B
        //��s�ɂ܂Ƃ߂��܂����A�ǐ��D��ł����Ă܂Ƃ߂Ă��܂���B
        card.Count--;

        //��������0�ɂȂ�����J�[�h���̂Ă�
        if (card.Count <= 0)
        {
            _cards.Remove(card);
            return true;
        }

        return false;
    }

    public int CalcDrawableCount()
    {
        return Capacity - _cards.Count;
    }

    public bool ContainsCard(Card card)
    {
        return _cards.Contains(card);
    }

    public int FindAll(Card card)
    {
        var list = _cards.FindAll(c => c.Equals(card));

        int count = 0;

        foreach (var item in list)
        {
            count += item.Count;
        }

        return count;
    }

    public void AddHandCapacity(int amount)
    {
        _addHandCapacityValue += amount;

        if (_addHandCapacityValue < 0) throw new System.ArgumentOutOfRangeException();
    }

    public void SetDefaultHandCapacity(int newCapacity)
    {
        _handCapacity = newCapacity;
    }
}

public class HandCapacityExceededException : System.Exception
{
    public HandCapacityExceededException()
    {
        Debug.LogError("��D�̃L���p�V�e�B�𒴂��Ēǉ����悤�Ƃ��܂����B�ǉ��̓L�����Z������܂����B");
    }
}