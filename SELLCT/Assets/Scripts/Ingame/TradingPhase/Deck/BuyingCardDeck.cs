using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyingCardDeck : IDeck
{
    //���݃^�[���ōw�������J�[�h�����������
    readonly List<Card> _cards = new();

    public void Add(Card card)
    {
        _cards.Add(card);
    }

    public Card Draw()
    {
        if (_cards.Count == 0) return EEX_null.Instance;

        //�g�b�v����1������
        Card card = _cards[0];
        _cards.RemoveAt(0);

        return card;
    }
    public bool ContainsCard(Card card)
    {
        return _cards.Contains(card);
    }
}
