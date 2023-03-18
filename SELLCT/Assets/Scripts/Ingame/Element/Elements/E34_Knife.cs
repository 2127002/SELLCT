using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class E34_Knife : Card
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
    
    [SerializeField] Image _E34CommandImage = default!;

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
        _E34CommandImage.gameObject.SetActive(_handMediator.ContainsCard(this));
    }

    public override void Buy()
    {
        _controller.DecreaseMoney(_parameter.GetMoney());
        _E34CommandImage.gameObject.SetActive(true);
    }

    public override void Passive()
    {
        //TODO：シーン3に遷移
        Debug.LogWarning(StringManager.ToDisplayString("命がなくなりました！シーン3に遷移する処理は未実装なため続行されます。"));
    }

    public override void Sell()
    {
        _controller.IncreaseMoney(_parameter.GetMoney());
        _E34CommandImage.gameObject.SetActive(false);
    }
}
