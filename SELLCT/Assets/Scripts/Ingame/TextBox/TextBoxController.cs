using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBoxController : MonoBehaviour
{
    [SerializeField] TextBoxView _textBoxView = default!;

    public async UniTask UpdateText(string speaker, string message)
    {
        _textBoxView.UpdateText(speaker, message);
        _textBoxView.UpdateDisplay();

        await _textBoxView.DisplayTextOneByOne();
    }

    public void Enable()
    {
        _textBoxView.Enable();
    }

    public void Disable()
    {
        _textBoxView.Disable();
    }
}
