using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTrader : MonoBehaviour, ITrader
{
    [SerializeField] TraderParameter _traderParameter;

    public string BuyMessage(Card card)
    {
        throw new System.NotImplementedException();
    }

    public string CardMessage(Card card)
    {
        throw new System.NotImplementedException();
    }

    public string EndMessage()
    {
        throw new System.NotImplementedException();
    }

    public string SellMessage(Card card)
    {
        throw new System.NotImplementedException();
    }

    public string StartMessage()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
