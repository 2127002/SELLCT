using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_3 : MonoBehaviour
{
    [SerializeField] EndingController _endingController;
    public void End_3Transition()
    {
        //TODO:ââèoí«â¡
        _endingController.StartEndingScene(EndingController.EndingScene.End3);
    }
}
