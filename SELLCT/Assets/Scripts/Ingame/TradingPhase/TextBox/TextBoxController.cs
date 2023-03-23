using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBoxController : MonoBehaviour
{
    [SerializeField] TextBoxView _textBoxView = default!;
    [SerializeField] E14_TextBox _e14 = default!;

    public async UniTask UpdateText(string speaker, string message)
    {
        _textBoxView.UpdateText(speaker, message);
        _textBoxView.UpdateDisplay(_e14.ContainsPlayerDeck);

        await _textBoxView.DisplayTextOneByOne();
    }
}
