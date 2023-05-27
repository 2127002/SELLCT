using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleLogo : MonoBehaviour
{
    [SerializeField] Sprite _normal = default!;
    [SerializeField] Sprite _none = default!;

    [SerializeField] Image _logo = default!;

    private void Awake()
    {
        Sprite sprite = StringManager.hasElements[(int)StringManager.Element.E21] ? _normal : _none;

        _logo.sprite = sprite;
    }
}
