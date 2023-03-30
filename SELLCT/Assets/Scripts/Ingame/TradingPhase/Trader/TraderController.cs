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

        //���̃g���[�_�[��I��
        Trader next = _traderEncounterController.Selection();
        SetTrader(next);
    }

    private void SetTrader(Trader trader)
    {
        _currentTrader = trader;

        //Sprite���g���[�_�[���ƂɕύX����
        //���ɕ\������ǉ�����邱�ƂɂȂ����炱�̏����ł͕�������܂���B
        _traderView.SetSprite(trader.TraderSprite);

        //�v���C���[�̓Ɣ��ɒu�������邩���肷��
        bool switchToPlayerMonologue = _playerMonologue.SwitchToPlayerMonologue;

        //�o��̃e�L�X�g�̏������肷��
        string speaker = switchToPlayerMonologue ? _playerMonologue.Speaker : trader.Name;
        string startMessage = switchToPlayerMonologue ? _playerMonologue.StartMessage() : trader.StartMessage();

        //�o��̃e�L�X�g��\������
        _textBoxController.UpdateText(speaker, startMessage).Forget();
    }

    public Trader CurrentTrader => _currentTrader;
}
