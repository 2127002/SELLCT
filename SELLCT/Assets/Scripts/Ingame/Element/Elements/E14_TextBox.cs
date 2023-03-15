using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class E14_TextBox : Card
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
    [SerializeField] Image _u5 = default!;
    [SerializeField] Image _u13 = default!;

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

    public override void Buy()
    {
        //TODO:SE301の再生
        //TODO:画面全体を脈動させるアニメーション
        //TODO:テキストボックスを更新する

        _controller.DecreaseMoney(_parameter.GetMoney());
        BuyTextBoxChecker();
    }

    public override void Passive()
    {
        //DoNothing
    }

    public override void Sell()
    {
        _controller.IncreaseMoney(_parameter.GetMoney());
        SellTextBoxChecker();
    }

    public void BuyTextBoxChecker()
    {
        if (_u5.enabled == false && _u13.enabled == false)
        {
            _u5.enabled = true;
            _u13.enabled = true;
        }
    }
    public void SellTextBoxChecker()
    {
        if (_handMediator.ContainsCard(this)) return;
        if (_u5.enabled == true && _u13.enabled == true)
        {
            _u5.enabled = false;
            _u13.enabled = false;
        }
    }
}
