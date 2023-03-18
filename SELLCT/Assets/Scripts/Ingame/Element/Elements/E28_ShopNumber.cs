using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E28_ShopNumber : Card
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
    [SerializeField] Hand _traderHand = default!;
    [SerializeField] PhaseController _phaseController = default!;

    readonly List<Sprite> result = new();

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
        int add = _handMediator.FindAll(this);
        _traderHand.AddHandCapacity(add);

        for (int i = 0; i < add; i++)
        {
            _handMediator.DrawCard();
        }
    }

    public override void Buy()
    {
        _controller.DecreaseMoney(_parameter.GetMoney());
        _traderHand.AddHandCapacity(1);
    }

    public override void Passive()
    {
        // DoNothing
    }

    public override void Sell()
    {
        _controller.IncreaseMoney(_parameter.GetMoney());
        _traderHand.AddHandCapacity(-1);
    }
}
