using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderView : MonoBehaviour
{
    [SerializeField] FadeInView _fadeinView;

    void Start()
    {
        _fadeinView.StartFade().Forget();
    }

}
