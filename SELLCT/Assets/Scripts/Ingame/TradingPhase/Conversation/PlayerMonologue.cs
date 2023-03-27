using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMonologue : MonoBehaviour, IConversationMessage
{
    [SerializeField] E16_Communication _e16 = default!;
    [SerializeField] E30_Name _e30 = default!;

    /// <summary>
    /// プレイヤーの独白に置き換えるか
    /// </summary>
    public bool SwitchToPlayerMonologue => !_e16.ContainsPlayerDeck;

    public string Speaker => _e30.CardName;

    public string StartMessage()
    {
        return "...誰だこいつ。手を差し出しているようだが…？";
    }

    public string EndMessage()
    {
        return "...思ったよりもいいヤツだったな。また会える機会があるかもしれない。";
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
