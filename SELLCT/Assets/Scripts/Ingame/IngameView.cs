using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameView : MonoBehaviour
{
    [SerializeField] FadeOutView _fadeOutView = default!;
    [SerializeField] Canvas _canvas;

    public void OnGameStart()
    {
        _canvas.enabled = true;

        _fadeOutView.StartFade().Forget();
    }
}
