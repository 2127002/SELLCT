using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeckUIView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _kanjiText = default!;
    [SerializeField] TextMeshProUGUI _numberText = default!;

    public void ChangeDeckCount(int deckCount)
    {
        _numberText.text = deckCount.ToString();
    }

    public void EnableNumber()
    {
        _numberText.enabled = true;
    }

    public void DisableNumber()
    {
        _numberText.enabled = false;
    }

    public void EnableKanji()
    {
        _kanjiText.enabled = true;
    }

    public void DisableKanji()
    {
        _kanjiText.enabled = false;
    }

}
