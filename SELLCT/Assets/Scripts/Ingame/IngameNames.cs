using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// インゲームの名前のリストがあるファーストコレクションクラスです。
/// </summary>
public class IngameNames : MonoBehaviour
{
    readonly List<string> _names = new();

    public void Add(string name)
    {
        //名前が空なら動作させない
        if (string.IsNullOrEmpty(name)) return;

        _names.Add(name);
    }

    public void AddRange(IEnumerable<string> names)
    {
        //不正値の読み取りのため、AddRangeではなく自前のAddを呼び出します。
        foreach (var item in names)
        {
            Add(item);
        }
    }

    public IReadOnlyList<string> Names => _names;
}
