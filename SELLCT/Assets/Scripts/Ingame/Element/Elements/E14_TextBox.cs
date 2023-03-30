using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class E14_TextBox : Card
{
    [SerializeField] TextBoxController _textBoxController = default!;
    [SerializeField] PhaseController _phaseController = default!;

    public override int Id => 14;

    private void Awake()
    {
        _phaseController.OnGameStart.Add(OnGameStart);
    }

    private void OnDestroy()
    {
        _phaseController.OnGameStart.Remove(OnGameStart);
    }

    private void OnGameStart()
    {
        if (ContainsPlayerDeck) _textBoxController.Enable();
        else _textBoxController.Disable();
    }

    public override void Buy()
    {
        _moneyPossessedCcontroller.DecreaseMoney(_parameter.GetMoney());
        _textBoxController.Enable();
    }

    public override void OnPressedU6Button()
    {
        throw new System.NotImplementedException();
    }

    public override void Sell()
    {
        _moneyPossessedCcontroller.IncreaseMoney(_parameter.GetMoney());
        if (_handMediator.ContainsCard(this)) return;

        _textBoxController.Disable();
    }
}
