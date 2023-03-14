using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderEncounterController : MonoBehaviour
{
    [SerializeField] TraderController _traderController = default!;
    [SerializeField] TradersInstance _tradersInstance = default!;

    private void Awake()
    {
        _traderController.SetTrader(_tradersInstance.Traders[1]);
    }
}
