using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DeckMediator : MonoBehaviour
{
    public abstract Card TakeDeckCard();
    public abstract void RemoveHandCard(Card card);
    public abstract void AddDeck(Card card);
    public abstract void RearrangeCardSlots();
    public abstract void AddBuyingDeck(Card card);
    /// <summary>
    /// �R�D����D�A�w������f�b�L�Ɏw��J�[�h�����݂��邩�Ԃ�
    /// </summary>
    /// <param name="card">���肵�����J�[�h</param>
    /// <returns>true:������</returns>
    public abstract bool ContainsCard(Card card);
    /// <summary>
    /// �R�D����D�A�w������f�b�L�Ɏw��J�[�h���������݂��邩�Ԃ�
    /// </summary>
    /// <param name="card">���肵�����J�[�h</param>
    /// <returns>���݂�������</returns>
    public abstract int FindAll(Card card);
}
