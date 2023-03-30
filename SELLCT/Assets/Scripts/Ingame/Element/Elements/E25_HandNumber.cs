using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E25_HandNumber : Card
{
    [SerializeField] Hand _playerHand = default!;
    [SerializeField] PhaseController _phaseController = default!;
    [SerializeField] CardUIInstance _cardUIInstance = default!;
    [SerializeField] CardUIGenerator _cardUIGenerator = default!;

    public override int Id => 25;

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
        int add = _handMediator.FindAll(this);
        _playerHand.AddHandCapacity(add);

        for (int i = 0; i < add; i++)
        {
            _handMediator.DrawCard();
        }
    }

    public override void Buy()
    {
        _moneyPossessedController.DecreaseMoney(_parameter.GetMoney());
        _playerHand.AddHandCapacity(1);

        //Handlerを作ってからカードを引く
        _cardUIGenerator.Generate();
        _handMediator.DrawCard();
    }

    public override void OnPressedU6Button()
    {
        throw new System.NotImplementedException();
    }

    public override void Sell()
    {
        _moneyPossessedController.IncreaseMoney(_parameter.GetMoney());
        _playerHand.AddHandCapacity(-1);

        //先に引いたカードをデッキに戻す
        _handMediator.AddDeck(_playerHand.Cards[^1]);
        _playerHand.Remove(_playerHand.Cards[^1]);

        _cardUIInstance.RemoveAt(_cardUIInstance.Handlers.Count - 1);
    }
}
