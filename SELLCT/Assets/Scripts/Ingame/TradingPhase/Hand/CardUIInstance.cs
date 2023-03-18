using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardUIInstance : MonoBehaviour
{
    List<CardUIHandler> _handlers = new();
    [SerializeField] CardUIHandler _firstHandler = default!;

    [SerializeField] NumberHandView _numberHandView = default!;
    [SerializeField] KanjiHandView _kanjiHandView = default!;
    [SerializeField] HiraganaHandView _hiraganaHandView = default!;
    [SerializeField] KatakanaHandView _katakanaHandView = default!;
    [SerializeField] AlphabetHandView _alphabetHandView = default!;

    private void Awake()
    {
        Add(_firstHandler);
    }

    public void Add(CardUIHandler handler)
    {
        _handlers.Add(handler);

        _numberHandView.Add(handler.CardImages[1]);
        _kanjiHandView.Add(handler.CardImages[2]);
        _hiraganaHandView.Add(handler.CardImages[3]);
        _katakanaHandView.Add(handler.CardImages[4]);
        _alphabetHandView.Add(handler.CardImages[5]);
    }

    public void Remove(CardUIHandler handler)
    {
        _handlers.Remove(handler);

        _numberHandView.Remove(handler.CardImages[1]);
        _kanjiHandView.Remove(handler.CardImages[2]);
        _hiraganaHandView.Remove(handler.CardImages[3]);
        _katakanaHandView.Remove(handler.CardImages[4]);
        _alphabetHandView.Remove(handler.CardImages[5]);

        Destroy(handler.gameObject);
    }

    public void RemoveAt(int index)
    {
        Remove(_handlers[index]);
    }

    public IReadOnlyList<CardUIHandler> Handlers => _handlers;
}
