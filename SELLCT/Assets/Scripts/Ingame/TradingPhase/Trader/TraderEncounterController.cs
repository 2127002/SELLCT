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
        //�O��̏��l���������V���ȃ��X�g���쐬����B
        var availableTraders = _tradersInstance.Traders.Where(t => t != _prebTrader).ToList();

        //�����_���ȏ��l��I������
        int index = Random.Range(0, availableTraders.Count);
        Trader selectedTrader = availableTraders[index];

        _prebTrader = selectedTrader;

        return selectedTrader;
    }
}
