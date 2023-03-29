using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E4_No : Card
{
    [SerializeField] ChoicesManager _choicesManager = default!;

    NoChoice _noChoice;

    //�f�b�L������ɍs����������Start�B��肪����������Awake�ɕύX����PhaseController�Ŏ��s���𒲐����Ă��������B
    private void Start()
    {
        _noChoice = new(ContainsPlayerDeck);
        _choicesManager.Add(_noChoice);
    }

    public override void Buy()
    {
        _controller.DecreaseMoney(_parameter.GetMoney());

        _choicesManager.Enable(_noChoice.Id);
    }

    public override void Passive()
    {
        // DoNothing
    }

    public override void Sell()
    {
        _controller.IncreaseMoney(_parameter.GetMoney());
        if (_handMediator.ContainsCard(this)) return;

        _choicesManager.Disable(_noChoice.Id);
    }
}
