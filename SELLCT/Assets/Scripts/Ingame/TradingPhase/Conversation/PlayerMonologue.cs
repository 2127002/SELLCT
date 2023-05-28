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

        string[] texts = _start.datas[index].Text;
        int[] face = _start.datas[index].Face;
        string[] name = _start.datas[index].Name;

        return new(texts, face, name);
    }

    public ConversationMessage EndMessage()
    {
        int index = Random.Range(0, _end.datas.Length);

        string[] texts = _end.datas[index].Text;
        int[] face = _end.datas[index].Face;
        string[] name = _end.datas[index].Name;

        return new(texts, face, name);
    }

    public ConversationMessage CardMessage(Card card)
    {
        int index = card.Id;

        string[] texts = _select.datas[index].Text;
        int[] face = _select.datas[index].Face;
        string[] name = _select.datas[index].Name;

        return new(texts, face, name);
    }

    public ConversationMessage BuyMessage(Card card)
    {
        int index = card.Id;

        string[] texts = _buy.datas[index].Text;
        int[] face = _buy.datas[index].Face;
        string[] name = _buy.datas[index].Name;

        return new(texts, face, name);
    }

    public ConversationMessage SellMessage(Card card)
    {
        int index = card.Id;

        string[] texts = _sell.datas[index].Text;
        int[] face = _sell.datas[index].Face;
        string[] name = _sell.datas[index].Name;

        return new(texts, face, name);
    }
}
