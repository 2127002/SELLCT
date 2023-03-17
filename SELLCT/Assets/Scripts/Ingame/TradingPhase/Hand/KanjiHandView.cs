using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KanjiHandView : MonoBehaviour
{
    readonly List<Image> _kanjiImages = new();


    public void Set()
    {
        foreach (var item in _kanjiImages)
        {
            item.enabled = StringManager.hasElements[(int)StringManager.Element.E18];
        }
    }

    public void Add(Image kanjiImage)
    {
        _kanjiImages.Add(kanjiImage);
    }
}
