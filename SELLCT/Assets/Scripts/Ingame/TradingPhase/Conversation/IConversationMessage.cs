using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConversationMessage
{
    ConversationMessage StartMessage();
    ConversationMessage EndMessage();
    ConversationMessage CardMessage(Card card);
    ConversationMessage BuyMessage(Card card);
    ConversationMessage SellMessage(Card card);
}
