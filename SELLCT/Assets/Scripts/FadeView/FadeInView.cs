using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

class FadeInView : MonoBehaviour
{
    [SerializeField] FadeTime _fadeTime;
    [SerializeField] List<Image> _images;

    const float MAX_ALPHA = 1f;
    const float MIN_ALPHA = 0f;

    private void Init()
    {
        foreach (var image in _images)
        {
            SetAlpha(image, MIN_ALPHA);
        }
    }

    public async UniTask StartFade()
    {
        var cancellationToken = this.GetCancellationTokenOnDestroy();

        await UniTask.Delay((int)(_fadeTime.WaitTime * 1000f), false, PlayerLoopTiming.Update, cancellationToken);
        Init();
        float progress = -1f;
        while (progress < MAX_ALPHA)
        {
            await UniTask.Yield(cancellationToken);

            _fadeTime.AdvanceProgress();

            progress = _fadeTime.Progress();

            foreach (var image in _images)
            {
                SetAlpha(image, progress);
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
