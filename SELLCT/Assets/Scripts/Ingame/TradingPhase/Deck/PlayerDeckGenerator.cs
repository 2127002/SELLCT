using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeckGenerator : MonoBehaviour
{
    [Header("�m��R�D")]
    [SerializeField] List<InitCardCount> _confirmedDeck = default!;
    [Header("�m���R�D")]
    [SerializeField] List<InitCardCount> _probabilityDeck = default!;
    [SerializeField, Min(0)] int _drawCount = default!;

    [SerializeField] HandMediator _handMediator = default!;
    [SerializeField] CardPool _cardPool = default!;

    //Edit > Project Settings > Script Execution Order�Ŏ��s���𒲐����Ă��܂��B
    private void Awake()
    {
        AddConfirmedDeck();
        AddProbabilityDeck();
    }

    //�m����菈��
    private void AddConfirmedDeck()
    {
        foreach (var item in _confirmedDeck)
        {
            for (int i = 0; i < item.InitCount; i++)
            {
                Card card = _cardPool.Draw(item.Card);

                if (card is EEX_null)
                {
                    Debug.LogError("�J�[�h�v�[���ɑ��݂��Ȃ��J�[�h���I������܂����B" + item.Card);
                    return;
                }

                _handMediator.AddDeck(card);
            }
        }
    }

    //�m�����菈��
    private void AddProbabilityDeck()
    {
        //�v�f�̃R�s�[
        List<InitCardCount> deck = new();
        foreach (var item in _probabilityDeck)
        {
            deck.Add(item);
        }

        for (int i = 0; i < _drawCount; i++)
        {
            int index = Random.Range(0, deck.Count);

            Card card = _cardPool.Draw(deck[index].Card);

            if (card is EEX_null)
            {
                Debug.LogError("�J�[�h�v�[���ɑ��݂��Ȃ��J�[�h���I������܂����B" + deck[index].Card);
                return;
            }

            deck.RemoveAt(index);

            _handMediator.AddDeck(card);
        }
    }
}