using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderDeckGenerator : MonoBehaviour
{
    [SerializeField] CardPool _cardPool = default!;

    [SerializeField] TradersInstance _tradersInstance = default!;

    //Edit > Project Settings > Script Execution Order�Ŏ��s���𒲐����Ă��܂��B
    private void Awake()
    {
        AddTrader2_7Deck();
        AddTrader1Deck();
    }

    //TR2�`7�̏���
    private void AddTrader2_7Deck()
    {
        for (int i = 1; i < _tradersInstance.Traders.Count; i++)
        {
            _tradersInstance.Traders[i].CreateDeck(_cardPool);
        }
    }

    //TR1�̏���
    private void AddTrader1Deck()
    {
        _tradersInstance.Traders[0].CreateDeck(_cardPool);
    }
}
