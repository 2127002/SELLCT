using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TraderParameter
{
    [SerializeField,Tooltip("���C�ɂ���G�������g")] List<Card> favoriteCards;
    [SerializeField] List<PriorityCard> priorityCards;
    [SerializeField, Min(0)] int initialDisplayItemCount;

}
