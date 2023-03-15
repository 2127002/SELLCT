using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class E1_Buy : Card
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
    [SerializeField] Color color = default!;
    [SerializeField] Image _u3Card1 = default!;
    [SerializeField] Image _u3Card2 = default!;
    [SerializeField] Image _u3Card3 = default!;
    [SerializeField] Image _u3Card4 = default!;
    [SerializeField] Selectable _u3Card1Selectable = default!;
    [SerializeField] Selectable _u3Card2Selectable = default!;
    [SerializeField] Selectable _u3Card3Selectable = default!;
    [SerializeField] Selectable _u3Card4Selectable = default!;

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

    public override void Buy()
    {
        _controller.DecreaseMoney(_parameter.GetMoney());

    }

    public override void Passive()
    {
        //TODO:SE2ÇÃçƒê∂
    }

    public override void Sell()
    {
        _controller.IncreaseMoney(_parameter.GetMoney());
        BuyChecker();
    }

    public void BuyChecker()
    {
        if (_handMediator.ContainsCard(this)) return;
        _u3Card1.color = color;
        _u3Card2.color = color;
        _u3Card3.color = color;
        _u3Card4.color = color;
        _u3Card1Selectable.interactable = false;
        _u3Card2Selectable.interactable = false;
        _u3Card3Selectable.interactable = false;
        _u3Card4Selectable.interactable = false;
        _u3Card1.GetComponent<LeftClickDetector>().enabled = false;
        _u3Card2.GetComponent<LeftClickDetector>().enabled = false;
        _u3Card3.GetComponent<LeftClickDetector>().enabled = false;
        _u3Card4.GetComponent<LeftClickDetector>().enabled = false;
    }
}
