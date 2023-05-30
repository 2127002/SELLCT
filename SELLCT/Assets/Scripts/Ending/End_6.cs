using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_6 : MonoBehaviour
{
    [SerializeField] EndingController _endingController;
    [SerializeField] int _seceneChangeCount = default!;
    int clickSellCount = 0;
    public void End_6Transition()
    {
        clickSellCount++;
        if (clickSellCount != _seceneChangeCount) return;
        //TODO:ââèoí«â¡
        _endingController.StartEndingScene(EndingController.EndingScene.End6);
    }
}
