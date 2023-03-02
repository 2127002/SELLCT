using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderController : MonoBehaviour
{
    [SerializeField] TextBoxView _textBox;

    ITrader _currentTrader;

    public void SetTrader(ITrader trader)
    {
        _currentTrader = trader;

        _textBox.UpdeteText(trader.StartMessage());
        _textBox.DisplayTextOneByOne().Forget();
    }

    public ITrader CurrentTrader => _currentTrader;
}
