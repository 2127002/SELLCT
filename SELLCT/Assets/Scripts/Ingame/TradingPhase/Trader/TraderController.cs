using Cysharp.Threading.Tasks;
using UnityEngine;

public class TraderController : MonoBehaviour
{
    [SerializeField] TraderView _traderView = default!;
    [SerializeField] TraderEncounterController _traderEncounterController = default!;
    [SerializeField] TextBoxController _textBoxController = default!;
    [SerializeField] PhaseController _phaseController = default!;
    [SerializeField] PlayerMonologue _playerMonologue = default!;

    Trader _currentTrader = default!;

    private void Awake()
    {
        _phaseController.OnTradingPhaseStart.Add(OnTradingPhaseStart);
    }

    private void OnDestroy()
    {
        _phaseController.OnTradingPhaseStart.Remove(OnTradingPhaseStart);
    }

    private void OnTradingPhaseStart()
    {
        _traderView.OnPhaseStart();

        //次のトレーダーを選択
        Trader next = _traderEncounterController.Selection();
        SetTrader(next);
    }

    private void SetTrader(Trader trader)
    {
        _currentTrader = trader;

        //Spriteをトレーダーごとに変更する
        //仮に表情差分が追加されることになったらこの処理では負いきれません。
        _traderView.SetSprite(trader.TraderSprite);

        //プレイヤーの独白に置き換えるか判定する
        bool switchToPlayerMonologue = _playerMonologue.SwitchToPlayerMonologue;

        //出会いのテキストの情報を決定する
        string speaker = switchToPlayerMonologue ? _playerMonologue.Speaker : trader.Name;
        string startMessage = switchToPlayerMonologue ? _playerMonologue.StartMessage() : trader.StartMessage();

        //出会いのテキストを表示する
        _textBoxController.UpdateText(speaker, startMessage).Forget();
    }

    public Trader CurrentTrader => _currentTrader;
}
