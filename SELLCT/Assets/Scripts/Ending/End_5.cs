using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_5 : MonoBehaviour
{
    [SerializeField] EndingController _endingController;
    public void End_5Transition()
    {
        //TODO:���o�ǉ�
        _endingController.StartEndingScene(EndingController.EndingScene.End5);
    }
}
