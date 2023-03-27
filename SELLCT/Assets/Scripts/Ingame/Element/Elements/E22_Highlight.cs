using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E22_Highlight : Card
{
    [SerializeField] CardParameter _parameter = default!;
    [SerializeField] MoneyPossessedController _controller = default!;
    [SerializeField] Sprite _baseSprite = default!;
    [SerializeField] Sprite _number = default!;
    [SerializeField] Sprite _chineseCharacters = default!;
    [SerializeField] Sprite _hiragana = default!;
    [SerializeField] Sprite _katakana = default!;
    [SerializeField] Sprite _alphabet = default!;
    [SerializeField] HandMediator _handMediator = default!;
    [SerializeField] HighlightController _highlightController = default!;
    [SerializeField] PhaseController _phaseController = default!;
    readonly List<Sprite> result = new();

    public override string CardName => _parameter.GetName();
    public override bool IsDisposedOfAfterSell => _parameter.IsDisposedOfAfterSell();
    public override int Rarity => _parameter.Rarity();
    public override IReadOnlyList<Sprite> CardSprite
    {
        get
        {
            //‰Šú‰»
            if (result.Count == 0)
            {
                result.Add(_baseSprite);
                result.Add(_number);
                result.Add(_chineseCharacters);
                result.Add(_hiragana);
                result.Add(_katakana);
                result.Add(_alphabet);
            }

            return result;
        }
    }
    public override bool ContainsPlayerDeck => _handMediator.ContainsCard(this);

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
