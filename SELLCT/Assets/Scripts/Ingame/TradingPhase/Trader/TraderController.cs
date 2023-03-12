using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderController : MonoBehaviour
{
    [SerializeField] TextBoxView _textBox;

    Trader _currentTrader;

    public void SetTrader(Trader trader)
    {
        _currentTrader = trader;

        _textBox.UpdeteText(trader.StartMessage());
        _textBox.DisplayTextOneByOne().Forget();
    }

    public Trader CurrentTrader => _currentTrader;
}
