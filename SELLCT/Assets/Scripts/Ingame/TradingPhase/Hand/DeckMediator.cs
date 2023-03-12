using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DeckMediator : MonoBehaviour
{
    public abstract Card TakeDeckCard();
    public abstract void RemoveHandCard(Card card);
    public abstract void AddDeck(Card card);
}
