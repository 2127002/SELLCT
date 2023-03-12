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
        return new(_initialFavorability);
    }

    public int InitalDeckCount()
    {
        return _initialDeckCount;
    }
}
