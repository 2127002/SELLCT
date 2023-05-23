using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_4 : MonoBehaviour
{
    [SerializeField] EndingController _endingController;
    public void End_4Transition()
    {
        //TODO:ââèoí«â¡
        _endingController.StartEndingScene(EndingController.EndingScene.End4);
    }
}
