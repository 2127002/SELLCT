using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckUIController : MonoBehaviour
{
    [SerializeField] DeckUIView _deckUIView = default!;

    private void Reset()
    {
        _deckUIView = GetComponent<DeckUIView>();
    }

    public void ChangeDeckCount(int deckCount)
    {
        _deckUIView.ChangeDeckCount(deckCount);
    }

    public void EnableNumber()
    {
        _deckUIView.EnableNumber();
    }

    public void DisableNumber()
    {
        _deckUIView.DisableNumber();
    }

    public void EnableKanji()
    {
        _deckUIView.EnableKanji();
    }

    public void DisableKanji()
    {
        _deckUIView.DisableKanji();
    }
}
