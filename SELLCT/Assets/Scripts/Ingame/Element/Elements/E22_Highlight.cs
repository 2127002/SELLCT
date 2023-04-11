using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E22_Highlight : Card
{
    [SerializeField] HighlightController _highlightController = default!;
    [SerializeField] PhaseController _phaseController = default!;

    public override int Id => 22;

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
        _highlightController.Enable();
    }

    public override void OnPressedU6Button()
    {
        throw new System.NotImplementedException();
    }

    public override void Sell()
    {
        if (_handMediator.ContainsCard(this)) return;

        _highlightController.Disable();
    }
}
