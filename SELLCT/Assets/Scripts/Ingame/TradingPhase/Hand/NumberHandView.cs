using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberHandView : MonoBehaviour
{
    readonly List<Image> _numberImages = new();

    public void Set()
    {
        foreach (var item in _numberImages)
        {
            item.enabled = StringManager.hasElements[(int)StringManager.Element.E17];
        }
    }

    public void Add(Image numberImage)
    {
        _numberImages.Add(numberImage);
    }
}
