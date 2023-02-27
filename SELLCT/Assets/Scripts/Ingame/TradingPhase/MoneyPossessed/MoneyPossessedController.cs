using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPossessedController : MonoBehaviour
{
    [SerializeField] int _defalutAmount = 100;
    Money _money;

    private void Awake()
    {
        _money = new(_defalutAmount);
    }

    public void DecreaseMoney(Money money)
    {
        if (money == null) throw new NullReferenceException();

        try
        {
            _money = _money.Subtract(money);
        }
        catch (NegativeMoneyException)
        {
            Debug.Log("éÿã‡íÜÇ»ÇÃÇ…Ç»Ç∫îÉÇ¶ÇÈÇ∆évÇ¡ÇΩÅB");
            return;
        }

        Debug.Log(_money.CurrentAmount());
    }

    public void IncreaseMoney(Money money)
    {
        if (money == null) throw new NullReferenceException();

        _money = _money.Add(money);

        Debug.Log(_money.CurrentAmount());
    }
}
