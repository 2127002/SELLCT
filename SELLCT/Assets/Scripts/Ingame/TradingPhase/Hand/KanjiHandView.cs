using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KanjiHandView : MonoBehaviour
{
    [SerializeField] List<Image> _numberImages;

    private void Awake()
    {
        Set();
    }

    public void Set()
    {
        foreach (var item in _numberImages)
        {
            item.enabled = StringManager.hasElements[1];
        }
    }
}
