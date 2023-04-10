using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E4_No : Card
{
    [SerializeField] ChoicesManager _choicesManager = default!;

    NoChoice _noChoice;

    public override int Id => 4;

    //�f�b�L������ɍs����������Start�B��肪����������Awake�ɕύX����PhaseController�Ŏ��s���𒲐����Ă��������B
    private void Start()
    {
        _noChoice = new(ContainsPlayerDeck);
        _choicesManager.Add(_noChoice);
    }

    public override void Buy()
    {
        _choicesManager.Enable(_noChoice.Id);
    }

    public override void OnPressedU6Button()
    {
        throw new System.NotImplementedException();
    }

    public override void Sell()
    {
        if (_handMediator.ContainsCard(this)) return;

        _choicesManager.Disable(_noChoice.Id);
    }
}
