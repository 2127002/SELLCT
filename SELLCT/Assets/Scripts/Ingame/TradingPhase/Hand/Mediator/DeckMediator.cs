using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DeckMediator : MonoBehaviour
{
    public abstract void DrawCard();
    public abstract bool RemoveHandCard(Card card);
    public abstract void AddPlayerDeck(Card card);
    public abstract void UpdateCardSprites();
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
    /// <summary>
    /// CardUIHandler�ɉ�������D�̃J�[�h���擾����
    /// </summary>
    /// <param name="handler">�J�[�h�̏����擾������CardUIHandler</param>
    /// <returns>�擾�����J�[�h</returns>
    public abstract Card GetCardAtCardUIHandler(CardUIHandler handler);
}
