using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//何も答えない選択肢
public class NothingChoice:ITalkChoice
{
    public int Id => 2;

    public string Text => "何も答えない";

    public bool Enabled => _enabled;

    bool _enabled;

    public NothingChoice(bool enabled)
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
            //無効時は隠すため、表示することがない。
            throw new System.NotImplementedException("この項目が無効状態時、クリックされる処理は定義されていません。");
        }

        Debug.Log(".......(何も答えない)");
    }
}
