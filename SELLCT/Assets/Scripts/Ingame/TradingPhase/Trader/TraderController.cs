using Cysharp.Threading.Tasks;
using UnityEngine;

public class TraderController : MonoBehaviour
{
    [SerializeField] TraderView _traderView = default!;
    [SerializeField] TraderEncounterController _traderEncounterController = default!;
    [SerializeField] PhaseController _phaseController = default!;
    [SerializeField] Hand _traderHand = default!;
    [SerializeField] ConversationController _conversationController = default!;

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

        //トレーダーによって初期展示数が異なる
        _traderHand.SetDefaultHandCapacity(trader.InitialDisplayItemCount);

        //トレーダーと出会った時の処理をおこなう。
        _conversationController.OnStart().Forget();
    }

    public Trader CurrentTrader => _currentTrader;
}
