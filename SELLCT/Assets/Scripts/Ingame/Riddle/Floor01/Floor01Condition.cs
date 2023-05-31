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

        await _textBoxController.UpdateText(null, "�����Ŏ��͒m�����B���������i�ނׂ������Ƃ炵�Ă���킯�ł͂Ȃ��Ƃ������Ƃ��B");
        await _textBoxController.UpdateText(null, "...���������ƁB�����͂����̔鋫�ł͂Ȃ��B�܂��A�������������E�ł��Ȃ��B");
        await _textBoxController.UpdateText(null, "�ƌ��������Ƃ��낾�����]�Ȑ܂����č��͑�l�w���B����A�܂����ɉ����ς��Ȃ��ȁB");
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
