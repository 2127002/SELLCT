using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E22_Highlight : Card
{
    [SerializeField] HighlightController _highlightController = default!;
    [SerializeField] PhaseController _phaseController = default!;

    private void Start()
    {
        _phaseController.OnGameStart.Add(Init);
    }
    
    private void OnDestroy()
    {
        _phaseController.OnGameStart.Remove(Init);
    }

    private void Init()
    {
        if (ContainsPlayerDeck) _highlightController.Enable();
        else _highlightController.Disable();
    }

    public override void Buy()
    {
        _controller.DecreaseMoney(_parameter.GetMoney());

        _highlightController.Enable();
    }

    public override void Passive()
    {
        // DoNothing
    }

    public override void Sell()
    {
        _controller.IncreaseMoney(_parameter.GetMoney());
        if (_handMediator.ContainsCard(this)) return;

        _highlightController.Disable();
    }
}
