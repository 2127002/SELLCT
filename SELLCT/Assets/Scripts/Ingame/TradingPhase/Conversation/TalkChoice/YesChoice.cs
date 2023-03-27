using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class YesChoice : ITalkChoice
{
    public int Id => 0;

    public string Text => "はい";

    public bool Enabled => _enabled;

    bool _enabled;

    public YesChoice(bool enabled)
    {
        _enabled = enabled;
    }

    public void Disable()
    {
        _enabled = false;
    }

    public void Enable()
    {
        _enabled = true;
    }

    public void Select()
    {
        if (!Enabled)
        {
            //TODO:SE2の再生
            Debug.Log("Yesは無効です");
            return;
        }

        Debug.Log("Yes!!");
    }
}
