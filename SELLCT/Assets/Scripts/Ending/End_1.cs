using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_1 : MonoBehaviour
{
    [SerializeField] EndingController _endingController;
    public void End_1Transition()
    {
        //TODO:���o�ǉ�
        _endingController.StartEndingScene(EndingController.EndingScene.End1);
    }
}
