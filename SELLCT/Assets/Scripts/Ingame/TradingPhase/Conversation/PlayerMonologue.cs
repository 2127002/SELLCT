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

    public ConversationMessage StartMessage()
    {
        int index = Random.Range(0, _start.datas.Length);

        string[] texts = _select.datas[index].Text;
        int[] face = _select.datas[index].Face;

        return new(texts, face);
    }

    public ConversationMessage EndMessage()
    {
        int index = Random.Range(0, _end.datas.Length);

        string[] texts = _end.datas[index].Text;
        int[] face = _end.datas[index].Face;

        return new(texts, face);
    }

    public ConversationMessage CardMessage(Card card)
    {
        int index = card.Id;

        string[] texts = _select.datas[index].Text;
        int[] face = _select.datas[index].Face;

        return new(texts, face);
    }

    public ConversationMessage BuyMessage(Card card)
    {
        int index = card.Id;

        string[] texts = _buy.datas[index].Text;
        int[] face = _buy.datas[index].Face;

        return new(texts, face);
    }

    public ConversationMessage SellMessage(Card card)
    {
        int index = card.Id;

        string[] texts = _sell.datas[index].Text;
        int[] face = _sell.datas[index].Face;

        return new(texts, face);
    }
}
