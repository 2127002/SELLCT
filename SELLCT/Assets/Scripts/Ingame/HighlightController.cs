using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HighlightController : MonoBehaviour
{
    public void Enable()
    {
        //個数は変更されるので毎回取得します
        //楽ですが効率があまり良くないので、仕様が確定した段階でより良くしてもいいかもしれません。
        var selectableHighlights = FindObjectsByType<Selectable>(FindObjectsInactive.Include, FindObjectsSortMode.None).Select(x => x.GetComponent<ISelectableHighlight>());

        foreach (var item in selectableHighlights)
        {
            item.EnableHighlight();
        }
    }

    public void Disable()
    {
        //個数は変更されるので毎回取得します
        //楽ですが効率があまり良くないので、仕様が確定した段階でより良くしてもいいかもしれません。
        var selectableHighlights = FindObjectsByType<Selectable>(FindObjectsInactive.Include, FindObjectsSortMode.None).Select(x => x.GetComponent<ISelectableHighlight>());

        foreach (var item in selectableHighlights)
        {
            item.DisableHighlight();
        }
    }
}
