using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E25_HandNumber : Card
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
    [SerializeField] Hand _playerHand = default!;
    [SerializeField] PhaseController _phaseController = default!;
    [SerializeField] CardUIInstance _cardUIInstance = default!;
    [SerializeField] CardUIGenerator _cardUIGenerator = default!;

    readonly List<Sprite> result = new();

    public override bool IsDisposedOfAfterSell => _parameter.IsDisposedOfAfterSell();
    public override int Rarity => _parameter.Rarity();
    public override IReadOnlyList<Sprite> CardSprite
    {
        get
        {
            //初期化
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
        _playerHand.AddHandCapacity(add);

        for (int i = 0; i < add; i++)
        {
            _handMediator.DrawCard();
        }
    }

    public override void Buy()
    {
        _controller.DecreaseMoney(_parameter.GetMoney());
        _playerHand.AddHandCapacity(1);

        //Handlerを作ってからカードを引く
        _cardUIGenerator.Generate();
        _handMediator.DrawCard();
    }

    public override void Passive()
    {
        // DoNothing
    }

    public override void Sell()
    {
        _controller.IncreaseMoney(_parameter.GetMoney());
        _playerHand.AddHandCapacity(-1);

        //先に引いたカードをデッキに戻す
        _handMediator.AddDeck(_playerHand.Cards[^1]);
        _playerHand.Remove(_playerHand.Cards[^1]);

        _cardUIInstance.RemoveAt(_cardUIInstance.Handlers.Count - 1);
    }
}
