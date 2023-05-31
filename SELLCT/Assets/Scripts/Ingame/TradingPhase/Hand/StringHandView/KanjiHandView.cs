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
            if (item.sprite == null) continue;

            item.enabled = StringManager.hasElements[(int)StringManager.Element.E18];
        }
    }

    public void Add(Image kanjiImage)
    {
        _kanjiImages.Add(kanjiImage);
    }

    public void Remove(Image image)
    {
        _kanjiImages.Remove(image);
    }

}
