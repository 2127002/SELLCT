using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : IDeck
{
    List<Card> _deck = new();

    public void Add(Card card)
    {
        _deck.Add(card);
    }

    public Card Draw()
    {
        if (_deck.Count == 0) return EEX_null.Instance;

        //�����_���ɕԂ����������Ă��܂��B
        //�Ӑ}�I�Ƀh���[�������ۂ͂��̕ӂɏ�����ǉ����Ă��������B
        int index = UnityEngine.Random.Range(0, _deck.Count);

        Card card = _deck[index];
        _deck.RemoveAt(index);

        return card;
    }
    public bool ContainsCard(Card card)
    {
        return _deck.Contains(card);
    }
}
