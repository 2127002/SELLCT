using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TraderParameter
{
    [SerializeField, Tooltip("���C�ɂ���G�������g")] List<Card> _favoriteCards;
    [Header("���I�m���F(���o�������J�[�h�� + priority)/�i�c�J�[�h�v�[�����j�ɂȂ�܂��B")]
    [SerializeField] List<PriorityCard> _priorityCards;
    [SerializeField, Min(0)] int _initialDisplayItemCount;
    [SerializeField, Range(0, 100)] int _initialFavorability;
    [SerializeField, Min(-1)] int _initialDeckCount;
    [SerializeField] int _addFavorabilityValue;
    [SerializeField] int _favoriteCardAddValue;
    [SerializeField] FavourableView _favourableView;

    Favorability initialFavorability;
    Favorability addFavorabilityValue;
    Favorability favoriteCardAddValue;

    public IReadOnlyList<Card> FavoriteCards()
    {
        return _favoriteCards;
    }

    public IReadOnlyList<PriorityCard> PriorityCards()
    {
        return _priorityCards;
    }

    public int InitialDisplayItemCount()
    {
        return _initialDisplayItemCount;
    }

    public Favorability InitialFavorability()
    {
        initialFavorability ??= new(_initialFavorability,_favourableView);
        return initialFavorability;
    }

    public int InitalDeckCount()
    {
        return _initialDeckCount;
    }

    public Favorability AddFavorabilityValue()
    {
        addFavorabilityValue ??= new(_addFavorabilityValue, _favourableView);
        return addFavorabilityValue;
    }

    public Favorability FavoriteCardAddValue()
    {
        favoriteCardAddValue ??= new(_favoriteCardAddValue, _favourableView);
        return favoriteCardAddValue;
    }
}
