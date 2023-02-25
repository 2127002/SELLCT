using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class FadeOutView : MonoBehaviour
{
    [SerializeField] FadeTime _fadeTime;
    [SerializeField] List<Image> _images;

    const float MAX_ALPHA = 1f;

    private void Init()
    {
        foreach (var image in _images)
        {
            SetAlpha(image, MAX_ALPHA);
        }
    }

    public async UniTask StartFade()
    {
        var cancellationToken = this.GetCancellationTokenOnDestroy();

        await UniTask.Delay((int)(_fadeTime.WaitTime * 1000f), false, PlayerLoopTiming.FixedUpdate, cancellationToken);
        
        Init();

        while (true)
        {
            await UniTask.Yield(cancellationToken);

            _fadeTime.AdvanceProgress();

            float progress = MAX_ALPHA - _fadeTime.Progress();

            foreach (var image in _images)
            {
                SetAlpha(image, progress);
            }

            if (progress == MAX_ALPHA)
            {
                enabled = false;
            }
        }
    }

    private void SetAlpha(Image image, float alpha)
    {
        Color c = image.color;
        c.a = alpha;
        image.color = c;
    }
}
