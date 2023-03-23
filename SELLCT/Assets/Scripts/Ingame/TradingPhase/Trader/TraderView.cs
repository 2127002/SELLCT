using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TraderView : MonoBehaviour
{
    [SerializeField] FadeInView _fadeinView = default!;
    [SerializeField] Image _traderImage = default!;

    public void OnPhaseStart()
    {
        _fadeinView.StartFade().Forget();
    }

    public void SetSprite(Sprite sprite)
    {
        _traderImage.sprite = sprite;
    }
}
