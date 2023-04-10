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
                handler.DisableSelectability(InteractableChange.Money);
            }
        }
        else
        {
            foreach (var handler in _traderCardUIInstance.Handlers)
            {
                handler.EnabledSelectebility(InteractableChange.Money);
            }
        }
    }

    /// <summary>
    /// �����������炵�܂��B
    /// </summary>
    /// <param name="money">���炵��������</param>
    /// <exception cref="ArgumentNullException">�^����ꂽ������null�̍ۂɃG���[�ɂȂ�܂�</exception>
    /// <exception cref="NegativeMoneyException">���݂̏��������s���l�̏ꍇ�ɕԂ��܂�</exception>
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
                handler.DisableSelectability(InteractableChange.Money);
            }
        }

        _view.ChangeMoneyText(_money);
    }

    public void IncreaseMoney(Money money)
    {
        if (money == null) throw new ArgumentNullException();

        Money prebMoney = _money;

        _money = _money.Add(money);

        CheckMoney(prebMoney);

        _view.ChangeMoneyText(_money);
    }

    private void CheckMoney(Money prebMoney)
    {
        //�����O�̂���������l�ȉ��Ȃ���s
        if (prebMoney > Money.Zero) return;

        //����Ŋ���l�𒴂�������s
        if (_money <= Money.Zero) return;
        
        foreach (var handler in _traderCardUIInstance.Handlers)
        {
            handler.EnabledSelectebility(InteractableChange.Money);
        }
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
