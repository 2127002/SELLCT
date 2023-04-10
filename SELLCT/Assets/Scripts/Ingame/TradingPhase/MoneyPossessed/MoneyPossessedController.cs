using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPossessedController : MonoBehaviour
{
    [SerializeField] int _defaultAmount = 100;
    [SerializeField] MoneyPossessedView _view = default!;
    [SerializeField] CardUIInstance _traderCardUIInstance = default!;
    [SerializeField] PhaseController _phaseController = default!;

    Money _money;

    private void Reset()
    {
        _view = GetComponent<MoneyPossessedView>();
    }

    private void Awake()
    {
        _money = new(_defaultAmount);
        _phaseController.OnTradingPhaseStart.Add(OnTradingPhaseStart);

        _view.ChangeMoneyText(_money);
    }

    private void OnDestroy()
    {
        _phaseController.OnTradingPhaseStart.Remove(OnTradingPhaseStart);
    }

    private void OnTradingPhaseStart()
    {
        if (_money <= Money.Zero)
        {
            foreach (var handler in _traderCardUIInstance.Handlers)
            {
                handler.DisableSelectability();
            }
        }
    }

    /// <summary>
    /// 所持金を減らします。
    /// </summary>
    /// <param name="money">減らしたいお金</param>
    /// <exception cref="ArgumentNullException">与えられたお金がnullの際にエラーになります</exception>
    /// <exception cref="NegativeMoneyException">現在の所持金が不正値の場合に返します</exception>
    public void DecreaseMoney(Money money)
    {
        if (money == null) throw new ArgumentNullException();

        try
        {
            _money = _money.Subtract(money);
        }
        catch (NegativeMoneyException)
        {
            throw new NegativeMoneyException();
        }

        if (_money <= Money.Zero)
        {
            foreach (var handler in _traderCardUIInstance.Handlers)
            {
                handler.DisableSelectability();
            }
        }

        _view.ChangeMoneyText(_money);
    }

    public void IncreaseMoney(Money money)
    {
        if (money == null) throw new ArgumentNullException();

        _money = _money.Add(money);
        if (_money > Money.Zero)
        {
            foreach (var handler in _traderCardUIInstance.Handlers)
            {
                handler.EnabledSelectebility();
            }
        }

        _view.ChangeMoneyText(_money);
    }

    public void EnableNumber()
    {
        _view.EnableNumber();
    }

    public void DisableNumber()
    {
        _view.DisableNumber();
    }

    public void EnableAlphabet()
    {
        _view.EnableAlphabet();
    }

    public void DisableAlphabet()
    {
        _view.DisableAlphabet();
    }
}
