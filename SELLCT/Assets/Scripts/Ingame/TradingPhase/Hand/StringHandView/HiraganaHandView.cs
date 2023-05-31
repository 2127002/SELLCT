using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiraganaHandView : MonoBehaviour
{
    readonly List<Image> _hiraganaImages = new();

    public void Set()
    {
        foreach (var item in _hiraganaImages)
        {
            if (item.sprite == null) continue;

            item.enabled = StringManager.hasElements[(int)StringManager.Element.E19];
        }
    }

    public void Add(Image hiraganaImage)
    {
        _hiraganaImages.Add(hiraganaImage);
    }

    public void Remove(Image image)
    {
        _hiraganaImages.Remove(image);
    }

}
