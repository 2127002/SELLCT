using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_Yes : Card
{
    [SerializeField] ChoicesManager _choicesManager = default!;

    YesChoice _yesChoice;

    public override int Id => 3;

    //�f�b�L������ɍs����������Start�B��肪����������Awake�ɕύX����PhaseController�Ŏ��s���𒲐����Ă��������B
    private void Start()
    {
        _yesChoice = new(ContainsPlayerDeck);
        _choicesManager.Add(_yesChoice);
    }

    public override void Buy()
    {
        _choicesManager.Enable(_yesChoice.Id);
    }

    public override void OnPressedU6Button()
    {
        throw new System.NotImplementedException();
    }

    public override void Sell()
    {
        if (_handMediator.ContainsCard(this)) return;

        _choicesManager.Disable(_yesChoice.Id);
    }
}
