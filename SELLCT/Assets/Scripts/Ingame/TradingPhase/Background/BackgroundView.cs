using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundView : MonoBehaviour
{
    [SerializeField] FadeInView _fadeinView;

    private void Start()
    {
        _fadeinView.StartFade().Forget();
    }
}
