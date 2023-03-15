using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E19_Hiragana : Card
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
    [SerializeField] HiraganaHandView _hiraganaHandView = default!;
    [SerializeField] PhaseController _phaseController = default!;

    readonly List<Sprite> result = new();

    public override bool IsDisposedOfAfterSell => _parameter.IsDisposedOfAfterSell();
    public override int Rarity => _parameter.Rarity();
    public override IReadOnlyList<Sprite> CardSprite
    {
        get
        {
            //èâä˙âª
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

    int elementIndex = (int)StringManager.Element.E19;

    private void Awake()
    {
        _phaseController.OnTradingPhaseStart.Add(OnPhaseStart);
    }

    private void OnPhaseStart()
    {
        StringManager.hasElements[elementIndex] = _handMediator.ContainsCard(this);
        _hiraganaHandView.Set();
    }

    private void OnDestroy()
    {
        _phaseController.OnTradingPhaseStart.Remove(OnPhaseStart);
    }

    public override void Buy()
    {
        StringManager.hasElements[elementIndex] = true;
        _controller.DecreaseMoney(_parameter.GetMoney());
    }

    public override void Passive()
    {
        // DoNothing
    }

    public override void Sell()
    {
        _controller.IncreaseMoney(_parameter.GetMoney());

        if (_handMediator.ContainsCard(this)) return;
        StringManager.hasElements[elementIndex] = false;
        _hiraganaHandView.Set();
    }
}
