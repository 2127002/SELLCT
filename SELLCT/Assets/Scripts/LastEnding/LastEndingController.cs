using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LastEndingController : MonoBehaviour
{
    public void End()
    {
        //�Q�[�����I������
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
