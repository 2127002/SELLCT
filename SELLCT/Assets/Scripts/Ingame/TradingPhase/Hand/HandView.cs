using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandView : MonoBehaviour
{
    [SerializeField] List<Image> _handImages;

    public void SetHandImageEnabled(int index,bool enabled)
    {
        if(index < 0 || index >= _handImages.Count)throw new System.ArgumentOutOfRangeException("index");

        _handImages[index].enabled = enabled;
    }
}
