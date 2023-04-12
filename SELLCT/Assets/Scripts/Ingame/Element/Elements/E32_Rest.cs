using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class E32_Rest : Card
{
    [SerializeField] ExplorationBackgroundController _explorationBackgroundController = default!;
    [SerializeField] PhaseController _phaseController = default!;

    public override int Id => 32;

    private void Awake()
    {
        _phaseController.OnGameStart.Add(OnGameStart);
    }

    private void OnDestroy()
    {
        _phaseController.OnGameStart.Remove(OnGameStart);
    }

    private void OnGameStart()
    {
        _explorationBackgroundController.SetActiveE32Command(_handMediator.ContainsCard(this));
    }

    public override void Buy()
    {
        _explorationBackgroundController.SetActiveE32Command(true);
    }

    public override void OnPressedU6Button()
    {
        _explorationBackgroundController.OnPressedU6Button();
    }

    public override void Sell()
    {
        if (_handMediator.ContainsCard(this)) return;
        _explorationBackgroundController.SetActiveE32Command(false);
    }
}
