using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_4 : MonoBehaviour
{
    [SerializeField] EndingController _endingController;
    public void End_4Transition()
    {
        //TODO:���o�ǉ�
        _endingController.StartEndingScene(EndingController.EndingScene.End4);
    }
}
