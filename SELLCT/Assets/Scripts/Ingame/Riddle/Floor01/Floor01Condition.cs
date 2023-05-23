using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor01Condition : MonoBehaviour
{
    [SerializeField] E32_Rest _e32;
    [SerializeField] E10_Light _e10;
    [SerializeField] E7_Move _e7;
    [SerializeField] TextBoxController _textBoxController;

    Floor01Answer01 _answer01;

    private void Awake()
    {
        _answer01 = new(_e32, _e10, _e7, this);
    }

    private bool _isSolved;

    public async void Solve()
    {
        if (_isSolved) return;

        _isSolved = true;

        await _textBoxController.UpdateText(null, "そこで私は知った。光だけが進むべき道を照らしているわけではないということを。");
        await _textBoxController.UpdateText(null, "...判ったこと。ここはただの秘境ではない。また、私が元いた世界でもない。");
        await _textBoxController.UpdateText(null, "と言いたいところだが紆余曲折あって今は第四層だ。いや、まぁ特に何も変わらないな。");
    }

    public void OnNextButtonPressed()
    {
        _answer01.Go();
    }

    public bool OnRest()
    {
        return _answer01.Rest();
    }
}
