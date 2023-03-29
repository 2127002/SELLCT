using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E26_Favourable : Card
{
    [SerializeField] FavourableView _favourableView = default!;
    [SerializeField] PhaseController _phaseController = default!;

    private void Awake()
    {
        _phaseController.OnTradingPhaseStart.Add(OnPhaseStart);
    }

    private void OnDestroy()
    {
        _phaseController.OnTradingPhaseStart.Remove(OnPhaseStart);
    }

    private void OnPhaseStart()
    {
        bool enabled = _handMediator.ContainsCard(this);

        _favourableView.SetActive(enabled);
    }

    public override void Buy()
    {
        _favourableView.SetActive(true);

        _controller.DecreaseMoney(_parameter.GetMoney());
    }

    public override void Passive()
    {
        throw new System.NotImplementedException();
    }

    public override void Sell()
    {
        //このエレメントが何も無いなら下記を実行
        if (!_handMediator.ContainsCard(this))
        {
            _favourableView.SetActive(false);
        }

        _controller.IncreaseMoney(_parameter.GetMoney());
    }
}
