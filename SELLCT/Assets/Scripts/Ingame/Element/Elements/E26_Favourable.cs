using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E26_Favourable : Card
{
    [SerializeField] CardParameter _parameter = default!;
    [SerializeField] MoneyPossessedController _controller = default!;
    [SerializeField] Sprite _cardSprite = default!;
    [SerializeField] FavourableView _favourableView = default!;
    [SerializeField] HandMediator _handMediator = default!;

    public override bool IsDisposedOfAfterSell => _parameter.IsDisposedOfAfterSell();
    public override int Rarity => _parameter.Rarity();
    public override Sprite CardSprite => _cardSprite;

    private void Awake()
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
        _favourableView.SetActive(false);

        _controller.IncreaseMoney(_parameter.GetMoney());
    }
}
