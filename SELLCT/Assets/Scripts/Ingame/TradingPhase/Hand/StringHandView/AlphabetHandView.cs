using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ***HandView はStringManager.Elementの値以外は同じなので、一つのクラスにまとめた方が良いでしょう
public class AlphabetHandView : MonoBehaviour
{
    readonly List<Image> _images = new();

    public void Set()
    {
        foreach (var item in _images)
        {
            if (item.sprite == null) continue;

            item.enabled = StringManager.hasElements[(int)StringManager.Element.E21];
        }
    }

    public void Add(Image image)
    {
        _images.Add(image);
    }

    public void Remove(Image image)
    {
        _images.Remove(image);
    }
}
