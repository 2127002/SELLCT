using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeckGenerator : MonoBehaviour
{
    [Header("確定山札")]
    [SerializeField] List<InitCardCount> _confirmedDeck = default!;
    [Header("確率山札")]
    [SerializeField] List<InitCardCount> _probabilityDeck = default!;
    [SerializeField, Min(0)] int _drawCount = default!;

    [SerializeField] HandMediator _handMediator = default!;
    CardPool _cardPool = default!;

    public void Generate(CardPool pool)
    {
        _cardPool = pool;

        AddConfirmedDeck();
        AddProbabilityDeck();
    }

    //確定入手処理
    private void AddConfirmedDeck()
    {
        foreach (var item in _confirmedDeck)
        {
            for (int i = 0; i < item.InitCount; i++)
            {
                Card card = _cardPool.Draw(item.Card);

                if (card.Id < 0)
                {
                    Debug.LogError("カードプールに存在しないカードが選択されました。" + item.Card);
                    return;
                }

                _handMediator.AddPlayerDeck(card);
            }
        }
    }

    //確率入手処理
    private void AddProbabilityDeck()
    {
        //要素のコピー
        List<InitCardCount> deck = new();
        foreach (var item in _probabilityDeck)
        {
            deck.Add(item);
        }

        for (int i = 0; i < _drawCount; i++)
        {
            int index = Random.Range(0, deck.Count);

            Card card = _cardPool.Draw(deck[index].Card);

            if (card.Id < 0)
            {
                Debug.LogError("カードプールに存在しないカードが選択されました。" + deck[index].Card);
                return;
            }

            deck.RemoveAt(index);

            _handMediator.AddPlayerDeck(card);
        }
    }
}
