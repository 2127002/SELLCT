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

        //Sprite���g���[�_�[���ƂɕύX����
        //���ɕ\������ǉ�����邱�ƂɂȂ����炱�̏����ł͕�������܂���B
        _traderView.SetSprite(trader.Sprite);

        //�o��̃e�L�X�g��\������
        _textBoxController.UpdateText(trader.Name, trader.StartMessage()).Forget();
    }

    public Trader CurrentTrader => _currentTrader;
}
