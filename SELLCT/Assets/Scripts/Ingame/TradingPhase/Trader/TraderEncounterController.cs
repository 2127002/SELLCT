using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TraderEncounterController : MonoBehaviour
{
    [SerializeField] TradersInstance _tradersInstance = default!;

    Trader _prebTrader = null;

    public Trader Selection()
    {
        //前回の商人を除いた新たなリストを作成する。
        var availableTraders = _tradersInstance.Traders.Where(t => t != _prebTrader).ToList();

        //ランダムな商人を選択する
        int index = Random.Range(0, availableTraders.Count);
        Trader selectedTrader = availableTraders[index];

        _prebTrader = selectedTrader;

        return selectedTrader;
    }
}
