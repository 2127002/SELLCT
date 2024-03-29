using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class TraderController : MonoBehaviour
{
    [SerializeField] TraderView _traderView = default!;
    [SerializeField] TraderEncounterController _traderEncounterController = default!;
    [SerializeField] PhaseController _phaseController = default!;
    [SerializeField] Hand _traderHand = default!;
    [SerializeField] TradingPhaseController _tradingPhaseController = default!;

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
        //次のトレーダーを選択
        Trader next = _traderEncounterController.Selection();
        SetTrader(next);

        //トレーダーのセット直後に実行
        _tradingPhaseController.OnSetTrader();
    }

    private void SetTrader(Trader trader)
    {
        _currentTrader = trader;

        //トレーダーによって初期展示数が異なる
        _traderHand.SetDefaultHandCapacity(trader.InitialDisplayItemCount);

        //オフセットを反映する
        _traderView.SetOffset(trader.Offset);
    }

    public void SetTraderSprite(Sprite sprite)
    {
        _traderView.SetSprite(sprite);
    }

    public Trader CurrentTrader => _currentTrader;
}
