using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Q�[�����ɓo�ꂷ��J�[�h�ꗗ���`����B
/// </summary>
public class CardPool : MonoBehaviour
{
    [SerializeField] List<InitCardCount> _cardCapacity;
    readonly List<Card> _cardPool = new();

    //Edit > Project Settings > Script Execution Order�Ŏ��s���𒲐����Ă��܂��B
    void Awake()
    {
        //������
        foreach (var item in _cardCapacity)
        {
            for (int i = 0; i < item.InitCount; i++)
            {
                _cardPool.Add(item.Card);
            }
        }
    }

    /// <summary>
    /// �w��̃J�[�h�����o������
    /// </summary>
    /// <param name="card">���o�������J�[�h</param>
    /// <returns></returns>
    public Card Draw(Card card)
    {
        if (!_cardPool.Remove(card)) return EEX_null.Instance;

        return card;
    }

    /// <summary>
    /// �擪����1���h���[���鏈��
    /// </summary>
    /// <returns></returns>
    public Card Draw()
    {
        if (_cardPool.Count == 0) return EEX_null.Instance;

        Card card = _cardPool[0];
        _cardPool.RemoveAt(0);

        return card;
    }

    public IReadOnlyList<Card> Pool()
    {
        return _cardPool;
    }
}
