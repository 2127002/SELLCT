using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KatakanaHandView : MonoBehaviour
{
    readonly List<Image> _katakanaImages = new();

    public void Set()
    {
        foreach (var item in _katakanaImages)
        {
            item.enabled = StringManager.hasElements[(int)StringManager.Element.E20];
        }
    }

    public void Add(Image katakanaImage)
    {
        _katakanaImages.Add(katakanaImage);
    }
}
