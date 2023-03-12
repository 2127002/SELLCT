using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradersInstance : MonoBehaviour
{
    [SerializeField] List<Trader> _traders = new();

    public IReadOnlyList<Trader> Traders => _traders;
}
