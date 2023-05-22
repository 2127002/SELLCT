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
        _traderView.OnPhaseStart();

        //���̃g���[�_�[��I��
        Trader next = _traderEncounterController.Selection();
        SetTrader(next);

        //�g���[�_�[�̃Z�b�g����Ɏ��s
        _tradingPhaseController.OnSetTrader();
    }

    private void SetTrader(Trader trader)
    {
        _currentTrader = trader;

        //Sprite���g���[�_�[���ƂɕύX����
        //���ɕ\������ǉ�����邱�ƂɂȂ����炱�̏����ł͕�������܂���B
        _traderView.SetSprite(trader.TraderSprite);

        //�g���[�_�[�ɂ���ď����W�������قȂ�
        _traderHand.SetDefaultHandCapacity(trader.InitialDisplayItemCount);
    }

    public Trader CurrentTrader => _currentTrader;
}
