using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConversationMessage
{
    string StartMessage();
    string EndMessage();
    string CardMessage(Card card);
    string BuyMessage(Card card);
    string SellMessage(Card card);
}
