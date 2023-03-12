using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandView : MonoBehaviour
{
    [SerializeField] List<Image> _handImages;

    //Edit > Project Settings > Script Execution Order‚ÅÀs‡‚ğ’²®‚µ‚Ä‚¢‚Ü‚·B
    private void Awake()
    {
        //‰Šú‚Í‘S‚ÄÁ‚·
        foreach (var item in _handImages)
        {
            item.enabled = false;
        }
    }

    public void SetHandImageEnabled(int index,bool enabled)
    {
        if(index < 0 || index >= _handImages.Count)throw new System.ArgumentOutOfRangeException("index");

        _handImages[index].enabled = enabled;
    }
}
