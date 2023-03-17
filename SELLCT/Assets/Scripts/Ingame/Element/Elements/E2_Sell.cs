using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class E2_Sell : Card
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
    [SerializeField] Color defaultColor = default!;
    [SerializeField] Color changeColor = default!;
    [SerializeField] Image _u3Card1 = default!;
    [SerializeField] Image _u3Card2 = default!;
    [SerializeField] Image _u3Card3 = default!;
    [SerializeField] Image _u3Card4 = default!;
    [SerializeField] Selectable _u3Card1Selectable = default!;
    [SerializeField] Selectable _u3Card2Selectable = default!;
    [SerializeField] Selectable _u3Card3Selectable = default!;
    [SerializeField] Selectable _u3Card4Selectable = default!;

    bool currentColor = true;
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
    public override bool ContainsPlayerDeck => _handMediator.ContainsCard(this);

    public override void Buy()
    {
        _controller.DecreaseMoney(_parameter.GetMoney());
        currentColor = true;
        SellChecker(currentColor);
    }

    public override void Passive()
    {
        //TODO:SE2ÇÃçƒê∂
    }

    public override void Sell()
    {
        if (_handMediator.ContainsCard(this)) return;
        _controller.IncreaseMoney(_parameter.GetMoney());
        currentColor = false;
        SellChecker(currentColor);
    }

    public void SellChecker(bool checker)
    {
        var u3childCard1 = _u3Card1.GetComponentsInChildren<Image>();
        var u3childCard2 = _u3Card2.GetComponentsInChildren<Image>();
        var u3childCard3 = _u3Card3.GetComponentsInChildren<Image>();
        var u3childCard4 = _u3Card4.GetComponentsInChildren<Image>();

        if(checker == true)
        {
            for (int i = 0; i < 6; i++)
            {
                u3childCard1[i].color = defaultColor;
                u3childCard2[i].color = defaultColor;
                u3childCard3[i].color = defaultColor;
                u3childCard4[i].color = defaultColor;
            }
            _u3Card1Selectable.interactable = true;
            _u3Card2Selectable.interactable = true;
            _u3Card3Selectable.interactable = true;
            _u3Card4Selectable.interactable = true;
            _u3Card1.GetComponent<LeftClickDetector>().enabled = true;
            _u3Card2.GetComponent<LeftClickDetector>().enabled = true;
            _u3Card3.GetComponent<LeftClickDetector>().enabled = true;
            _u3Card4.GetComponent<LeftClickDetector>().enabled = true;
            Debug.Log("ï\é¶");
        }
        else
        {
            for (int i = 0; i < 6; i++)
            {
                u3childCard1[i].color = changeColor;
                u3childCard2[i].color = changeColor;
                u3childCard3[i].color = changeColor;
                u3childCard4[i].color = changeColor;
            }
            _u3Card1Selectable.interactable = false;
            _u3Card2Selectable.interactable = false;
            _u3Card3Selectable.interactable = false;
            _u3Card4Selectable.interactable = false;
            _u3Card1.GetComponent<LeftClickDetector>().enabled = false;
            _u3Card2.GetComponent<LeftClickDetector>().enabled = false;
            _u3Card3.GetComponent<LeftClickDetector>().enabled = false;
            _u3Card4.GetComponent<LeftClickDetector>().enabled = false;
            Debug.Log("îÒï\é¶");
        }
    }
}
