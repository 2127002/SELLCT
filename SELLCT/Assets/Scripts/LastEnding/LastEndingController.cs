using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LastEndingController : MonoBehaviour
{
    public void End()
    {
        //ÉQÅ[ÉÄÇèIóπÇ∑ÇÈ
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
