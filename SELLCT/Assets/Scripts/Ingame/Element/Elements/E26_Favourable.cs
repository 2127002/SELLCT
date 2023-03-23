using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E26_Favourable : Card
{
    [SerializeField] CardParameter _parameter = default!;
    [SerializeField] MoneyPossessedController _controller = default!;
    [SerializeField] Sprite _baseSprite = default!;
    [SerializeField] Sprite _number = default!;
    [SerializeField] Sprite _chineseCharacters = default!;
    [SerializeField] Sprite _hiragana = default!;
    [SerializeField] Sprite _katakana = default!;
    [SerializeField] Sprite _alphabet = default!;
    [SerializeField] FavourableView _favourableView = default!;
    [SerializeField] HandMediator _handMediator = default!;
    [SerializeField] PhaseController _phaseController = default!;

    readonly List<Sprite> result = new();

    public override string CardName => _parameter.GetName();
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
        // Do Nothing
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
