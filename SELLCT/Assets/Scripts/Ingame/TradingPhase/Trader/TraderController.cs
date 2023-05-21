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

        //���̃g���[�_�[��I��
        Trader next = _traderEncounterController.Selection();
        SetTrader(next);
    }

    private void SetTrader(Trader trader)
    {
        _currentTrader = trader;

        //�g���[�_�[�ɂ���ď����W�������قȂ�
        _traderHand.SetDefaultHandCapacity(trader.InitialDisplayItemCount);

        //�g���[�_�[�Əo��������̏����������Ȃ��B
        _conversationController.OnStart().Forget();
    }

    public void SetTraderSprite(Sprite sprite)
    {
        _traderView.SetSprite(sprite);
    }

    public Trader CurrentTrader => _currentTrader;
}
