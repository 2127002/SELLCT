using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TraderController : MonoBehaviour
{
    [SerializeField] TraderView _traderView = default!;
    [SerializeField] TextBoxController _textBoxController = default!;
    [SerializeField] PhaseController _phaseController = default!;
    [SerializeField] PlayerMonologue _playerMonologue = default!;

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

        //Spriteをトレーダーごとに変更する
        //仮に表情差分が追加されることになったらこの処理では負いきれません。
        _traderView.SetSprite(trader.Sprite);

        //プレイヤーの独白に置き換えるか判定する
        string speaker = _playerMonologue.SwitchToPlayerMonologue ?
            _playerMonologue.Speaker : trader.Name;

        string startMessage = _playerMonologue.SwitchToPlayerMonologue ?
            _playerMonologue.StartMessage() : trader.StartMessage();

        //出会いのテキストを表示する
        _textBoxController.UpdateText(speaker, startMessage).Forget();
    }

    public Trader CurrentTrader => _currentTrader;
}
