using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TraderView : MonoBehaviour
{
    [SerializeField] FadeInView _fadeinView = default!;
    [SerializeField] Image _traderImage = default!;

    Vector2 _defaultSizeDelta;
    Vector3 _defaultPos;

    private void Awake()
    {
        _defaultSizeDelta = _traderImage.rectTransform.sizeDelta;
        _defaultPos = _traderImage.rectTransform.position;
    }

    public void OnPhaseStart()
    {
        _fadeinView.StartFade().Forget();
    }

    public void SetSprite(Sprite sprite)
    {
        _traderImage.sprite = sprite;
    }

    public void SetOffset(TraderOffset offset)
    {
        _traderImage.rectTransform.sizeDelta = _defaultSizeDelta;
        _traderImage.rectTransform.position = _defaultPos;

        _traderImage.rectTransform.sizeDelta *= offset.ScaleRatio;
        _traderImage.rectTransform.position += offset.OffsetPos;
    }
}
