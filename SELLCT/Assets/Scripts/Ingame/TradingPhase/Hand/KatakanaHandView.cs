using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KatakanaHandView : MonoBehaviour
{
    [SerializeField] List<Image> _katakanaImages;

    private void Awake()
    {
        Set();
    }

    public void Set()
    {
        foreach (var item in _katakanaImages)
        {
            item.enabled = StringManager.hasElements[3];
        }
    }
}
