using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FavourableView : MonoBehaviour
{
    [SerializeField] Image _image;
    [SerializeField] List<Sprite> _sprites;

    public void Set(Favorability.Classification favorabiltyClass)
    {
        _image.sprite = _sprites[(int)favorabiltyClass];
    }

    public void SetActive(bool enabled)
    {
        _image.enabled = enabled;
    }
}
