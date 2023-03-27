using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NothingChoiceController : MonoBehaviour
{
    [SerializeField] E3_Yes _e3 = default!;
    [SerializeField] E4_No _e4 = default!;
    [SerializeField] ChoicesManager _choiceManager = default!;

    private void Start()
    {
        bool contains = _e3.ContainsPlayerDeck || _e4.ContainsPlayerDeck;
        ITalkChoice talkChoice = new NothingChoice(!contains);

        _choiceManager.Add(talkChoice);
    }
}
