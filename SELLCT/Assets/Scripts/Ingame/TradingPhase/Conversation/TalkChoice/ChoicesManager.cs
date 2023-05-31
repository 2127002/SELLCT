using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームにおけるすべての選択肢を管理するクラス
/// </summary>
public class ChoicesManager : MonoBehaviour
{
    readonly List<ITalkChoice> _choices = new();

    public void Add(ITalkChoice choice)
    {
        _choices.Add(choice);
    }

    public void Enable(int id)
    {
        _choices[id].Enable();
    }

    public void Disable(int id)
    {
        _choices[id].Disable();
    }

    public ITalkChoice GetTalkChoice(int id)
    {
        if (_choices.Count <= id || _choices[id] == null) throw new System.NullReferenceException("登録されていないID" + id + "が選択されました。設定を見直してください。");

        return _choices[id];
    }
}
