using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextFadeOutView : MonoBehaviour
{
    [SerializeField] FadeTime _fadeTime;
    [SerializeField] List<TextMeshProUGUI> _texts;

    const float MAX_ALPHA = 1f;
    const float MIN_ALPHA = 0;

    private void Init()
    {
        foreach (var text in _texts)
        {
            SetAlpha(text, MIN_ALPHA);
        }
    }

    public async UniTask StartFade()
    {
        var cancellationToken = this.GetCancellationTokenOnDestroy();

        await UniTask.Delay((int)(_fadeTime.WaitTime * 1000f), false, PlayerLoopTiming.Update, cancellationToken);
        Init();
        float progress = -1;
        while (progress != MAX_ALPHA)
        {
            await UniTask.Yield(cancellationToken);

            _fadeTime.AdvanceProgress();

            progress = _fadeTime.Progress();

            foreach (var text in _texts)
            {
                SetAlpha(text, MAX_ALPHA - progress);
            }
        }
    }

    private void SetAlpha(TextMeshProUGUI text, float alpha)
    {
        Color c = text.color;
        c.a = alpha;
        text.color = c;
    }

}
