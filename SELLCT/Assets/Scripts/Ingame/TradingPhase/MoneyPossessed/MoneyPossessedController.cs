using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPossessedController : MonoBehaviour
{
    [SerializeField] int _defaultAmount = 100;
    [SerializeField] MoneyPossessedView _view = default!;
    Money _money;

    private void Reset()
    {
        _view = GetComponent<MoneyPossessedView>();
    }

    private void Awake()
    {
        _money = new(_defaultAmount);
        _view.ChangeMoneyText(_money);
    }

    public void DecreaseMoney(Money money)
    {
        if (money == null) throw new ArgumentNullException();

        try
        {
            _money = _money.Subtract(money);
        }
        catch (NegativeMoneyException)
        {
            Debug.Log("éÿã‡íÜÇ»ÇÃÇ…Ç»Ç∫îÉÇ¶ÇÈÇ∆évÇ¡ÇΩÅB");
            return;
        }

        _view.ChangeMoneyText(_money);
    }

    public void IncreaseMoney(Money money)
    {
        if (money == null) throw new ArgumentNullException();

        _money = _money.Add(money);

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
