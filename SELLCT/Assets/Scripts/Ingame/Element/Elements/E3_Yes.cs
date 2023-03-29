using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_Yes : Card
{
    [SerializeField] ChoicesManager _choicesManager = default!;

    YesChoice _yesChoice;

    //デッキ生成後に行いたいためStart。問題が発生したらAwakeに変更してPhaseControllerで実行順を調整してください。
    private void Start()
    {
        _yesChoice = new(ContainsPlayerDeck);
        _choicesManager.Add(_yesChoice);
    }

    public override void Buy()
    {
        _controller.DecreaseMoney(_parameter.GetMoney());

        _choicesManager.Enable(_yesChoice.Id);
    }

    public override void Passive()
    {
        throw new System.NotImplementedException();
    }

    public override void Sell()
    {
        _controller.IncreaseMoney(_parameter.GetMoney());
        if (_handMediator.ContainsCard(this)) return;

        _choicesManager.Disable(_yesChoice.Id);
    }
}
