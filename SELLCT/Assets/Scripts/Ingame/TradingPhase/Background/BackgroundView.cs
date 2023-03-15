using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundView : MonoBehaviour
{
    [SerializeField] FadeInView _fadeinView;

    public void OnPhaseStart()
    {
        _fadeinView.StartFade().Forget();
    }
}
