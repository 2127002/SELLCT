using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TraderParameter
{
    [Header("���C�ɓ���G�������g�̓o�^")]
    [SerializeField] FavoriteCards _favoriteCard; 

    [Header("�D�撊�I�G�������g�̓o�^\n���I�m���F(���o�������J�[�h�� + priority)/�i�c�J�[�h�v�[�����j�ɂȂ�܂��B")]
    [SerializeField] List<PriorityCard> _priorityCards;

    [Header("�������i�񎦖���")]
    [SerializeField, Min(0)] int _initialDisplayItemCount;

    [Header("�����f�b�L�����B-1��I�������疳���ɂȂ�܂��B")]
    [SerializeField, Min(-1)] int _initialDeckCount;

    [Header("�����D���x")]
    [SerializeField, Range(0, 100)] int _initialFavorability;

    [Header("�������ɏ㏸����D���x�̒l�ł��B")]
    [SerializeField] int _addFavorabilityValue;

    [Header("�����̖��O�ł��B�g���Ȃ����������邱�Ƃɒ��ӂ��Ă��������B�i���ꕶ���Ȃǁj")]
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
