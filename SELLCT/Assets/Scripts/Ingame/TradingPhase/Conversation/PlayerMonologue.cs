using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMonologue : MonoBehaviour, IConversationMessage
{
    [SerializeField] E30_Name _e30 = default!;
    [SerializeField] ConversationDataBase _start;
    [SerializeField] ConversationDataBase _end;
    [SerializeField] ConversationDataBase _select;
    [SerializeField] ConversationDataBase _buy;
    [SerializeField] ConversationDataBase _sell;

    public string Speaker => _e30.CardName;

    public string[] StartMessage()
    {
        int index = Random.Range(0, _start.datas.Length);

        return _start.datas[index].Text;
    }

    public string[] EndMessage()
    {
        int index = Random.Range(0, _end.datas.Length);

        return _end.datas[index].Text;
    }

    public string[] CardMessage(Card card)
    {
        int index = card.Id;

        return _select.datas[index].Text;
    }

    public string[] BuyMessage(Card card)
    {
        int index = card.Id;

        return _buy.datas[index].Text;
    }

    public string[] SellMessage(Card card)
    {
        int index = card.Id;

        return _sell.datas[index].Text;
    }
}
