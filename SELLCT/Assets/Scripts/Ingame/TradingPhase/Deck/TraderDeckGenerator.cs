using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderDeckGenerator : MonoBehaviour
{
    [SerializeField] TradersInstance _tradersInstance = default!;

    CardPool _cardPool = default!;

    public void Generate(CardPool pool)
    {
        _cardPool = pool;

        AddTrader2_7Deck();
        AddTrader1Deck();
    }

    //TR2`7‚Ìˆ—
    private void AddTrader2_7Deck()
    {
        for (int i = 1; i < _tradersInstance.Traders.Count; i++)
        {
            _tradersInstance.Traders[i].CreateDeck(_cardPool);
        }
    }

    //TR1‚Ìˆ—
    private void AddTrader1Deck()
    {
        _tradersInstance.Traders[0].CreateDeck(_cardPool);
    }
}
