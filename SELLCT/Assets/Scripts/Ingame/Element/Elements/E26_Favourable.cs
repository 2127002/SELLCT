using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E26_Favourable : Card
{
    [SerializeField] FavourableView _favourableView = default!;
    [SerializeField] PhaseController _phaseController = default!;

    public override int Id => 26;

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
    }

    public override void OnPressedU6Button()
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
    }
}
