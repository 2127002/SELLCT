using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E4_No : Card
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
    [SerializeField] ChoicesManager _choicesManager = default!;
    readonly List<Sprite> result = new();

    NoChoice _noChoice;

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

    //デッキ生成後に行いたいためStart。問題が発生したらAwakeに変更してPhaseControllerで実行順を調整してください。
    private void Start()
    {
        _noChoice = new(ContainsPlayerDeck);
        _choicesManager.Add(_noChoice);
    }

    public override void Buy()
    {
        _controller.DecreaseMoney(_parameter.GetMoney());

        _choicesManager.Enable(_noChoice.Id);
    }

    public override void Passive()
    {
        // DoNothing
    }

    public override void Sell()
    {
        _controller.IncreaseMoney(_parameter.GetMoney());
        if (_handMediator.ContainsCard(this)) return;

        _choicesManager.Disable(_noChoice.Id);
    }
}
