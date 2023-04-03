using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TraderParameter
{
    [Header("お気に入りエレメントの登録")]
    [SerializeField] FavoriteCards _favoriteCard; 

    [Header("優先抽選エレメントの登録\n抽選確率：(取り出したいカード数 + priority)/（残カードプール数）になります。")]
    [SerializeField] List<PriorityCard> _priorityCards;

    [Header("初期商品提示枚数")]
    [SerializeField, Min(0)] int _initialDisplayItemCount;

    [Header("初期デッキ枚数。-1を選択したら無限になります。")]
    [SerializeField, Min(-1)] int _initialDeckCount;

    [Header("初期好感度")]
    [SerializeField, Range(0, 100)] int _initialFavorability;

    [Header("売買時に上昇する好感度の値です。")]
    [SerializeField] int _addFavorabilityValue;

    [Header("初期の名前です。使えない文字があることに注意してください。（特殊文字など）")]
    [SerializeField] string _defaultName;

    [SerializeField] FavourableView _favourableView;
    string _name;

    Favorability initialFavorability;
    Favorability addFavorabilityValue;
    Favorability favoriteCardBonus;

    public IReadOnlyList<Card> FavoriteCards => _favoriteCard.FavoriteCardsList;

    public IReadOnlyList<PriorityCard> PriorityCards => _priorityCards;

    public Favorability InitialFavorability
    {
        get
        {
            initialFavorability ??= new(_initialFavorability, _favourableView);
            return initialFavorability;
        }
    }

    public int InitialDisplayItemCount => _initialDisplayItemCount;

    public int InitalDeckCount => _initialDeckCount;

    public Favorability AddFavorabilityValue
    {
        get
        {
            addFavorabilityValue ??= new(_addFavorabilityValue, _favourableView);
            return addFavorabilityValue;
        }
    }

    public Favorability FavoriteCardBonus
    {
        get
        {
            favoriteCardBonus ??= new(_favoriteCard.Bonus, _favourableView);
            return favoriteCardBonus;
        }
    }

    public string DefaultName => _defaultName;
    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrEmpty(value)) throw new System.ArgumentNullException("value");

            _name = value;
        }
    }
}
