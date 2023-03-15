using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderController : MonoBehaviour
{
    [SerializeField] TraderView _traderView = default!;
    [SerializeField] TextBoxView _textBox = default!;
    [SerializeField] PhaseController _phaseController = default!;

    Trader _currentTrader = default!;

    private void Awake()
    {
        _phaseController.OnTradingPhaseStart.Add(_traderView.OnPhaseStart);
    }

    private void OnDestroy()
    {
        _phaseController.OnTradingPhaseStart.Remove(_traderView.OnPhaseStart);
    }

    public void SetTrader(Trader trader)
    {
        _currentTrader = trader;

        _textBox.UpdeteText(trader.StartMessage());
        _textBox.DisplayTextOneByOne().Forget();
    }

    public Trader CurrentTrader => _currentTrader;
}
