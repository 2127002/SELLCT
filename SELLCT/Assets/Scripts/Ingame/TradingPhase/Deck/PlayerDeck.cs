using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : IDeck
{
    List<Card> _deck = new();
    DeckUIController _deckUIController = default!;

    public PlayerDeck(DeckUIController deckUIController)
    {
        _deckUIController = deckUIController;
    }

    public void Add(Card card)
    {
        _deck.Add(card);
        _deckUIController.ChangeDeckCount(_deck.Count);
    }

    public Card Draw()
    {
        if (_deck.Count == 0) return EEX_null.Instance;

        //�����_���ɕԂ����������Ă��܂��B
        //�Ӑ}�I�Ƀh���[�������ۂ͂��̕ӂɏ�����ǉ����Ă��������B
        //int index = UnityEngine.Random.Range(0, _deck.Count);
        int index = 0;

        Card card = _deck[index];
        _deck.RemoveAt(index);
        _deckUIController.ChangeDeckCount(_deck.Count);

        return card;
    }

    public bool ContainsCard(Card card)
    {
        return _deck.Contains(card);
    }

    public int FindAll(Card card)
    {
        var list = _deck.FindAll(c => c.Equals(card));

        int count = 0;

        foreach (var item in list)
        {
            count += item.Count;
        }

        return count;
    }
}
