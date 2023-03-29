using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E4_No : Card
{
    [SerializeField] ChoicesManager _choicesManager = default!;

    NoChoice _noChoice;

    //デッキ生成後に行いたいためStart。問題が発生したらAwakeに変更してPhaseControllerで実行順を調整してください。
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
