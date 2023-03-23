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
    [SerializeField] string _defalutName;
    string _name;

    Favorability initialFavorability;
    Favorability addFavorabilityValue;
    Favorability favoriteCardAddValue;

    public IReadOnlyList<Card> FavoriteCards => _favoriteCards;

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

    public Favorability FavoriteCardAddValue
    {
        get
        {
            favoriteCardAddValue ??= new(_favoriteCardAddValue, _favourableView);
            return favoriteCardAddValue;
        }
    }

    public string DefalutName => _defalutName;
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
