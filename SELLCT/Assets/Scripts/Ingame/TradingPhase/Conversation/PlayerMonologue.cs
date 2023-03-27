using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMonologue : MonoBehaviour, IConversationMessage
{
    [SerializeField] E16_Communication _e16 = default!;
    [SerializeField] E30_Name _e30 = default!;

    /// <summary>
    /// �v���C���[�̓Ɣ��ɒu�������邩
    /// </summary>
    public bool SwitchToPlayerMonologue => !_e16.ContainsPlayerDeck;

    public string Speaker => _e30.CardName;

    public string StartMessage()
    {
        return "...�N�������B��������o���Ă���悤�����c�H";
    }

    public string EndMessage()
    {
        return "...�v���������������c�������ȁB�܂����@����邩������Ȃ��B";
    }

    public string CardMessage(Card card)
    {
        throw new System.NotImplementedException();
    }

    public string BuyMessage(Card card)
    {
        throw new System.NotImplementedException();
    }

    public string SellMessage(Card card)
    {
        throw new System.NotImplementedException();
    }
}
