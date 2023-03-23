using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class E32_Rest : Card
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
    [SerializeField] PhaseController _phaseController = default!;

    [Header("探索フェーズU6")]
    [SerializeField] Image _E32CommandImage = default!;
    [SerializeField] Sprite _restSprite = default!;
    [SerializeField] Sprite _wakeUpSprite = default!;

    [SerializeField] ExplorationBackgroundView _backgroundView = default!;

    readonly List<Sprite> result = new();

    bool _isRest = false;

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
        _phaseController.OnGameStart.Add(OnGameStart);
    }

    private void OnDestroy()
    {
        _phaseController.OnGameStart.Remove(OnGameStart);
    }

    private void OnGameStart()
    {
        _E32CommandImage.gameObject.SetActive(_handMediator.ContainsCard(this));
    }

    public override void Buy()
    {
        _controller.DecreaseMoney(_parameter.GetMoney());
        _E32CommandImage.gameObject.SetActive(true);
    }

    public override void Passive()
    {
        if (_isRest)
        {
            _E32CommandImage.sprite = _wakeUpSprite;
            _backgroundView.OnRest();
            _isRest = false;
        }
        else
        {
            _E32CommandImage.sprite = _restSprite;
            _backgroundView.OnWakeUp();
            _isRest = true;
        }
    }

    public override void Sell()
    {
        _controller.IncreaseMoney(_parameter.GetMoney());

        if (_handMediator.ContainsCard(this)) return;
        _E32CommandImage.gameObject.SetActive(false);
    }
}
