using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class RugController : MonoBehaviour
{
    [SerializeField] Canvas _normalRugCanvas = default!;
    [SerializeField] Canvas _animationRugCanvas = default!;

    [SerializeField] GameObject _animationRugStart = default!;
    [SerializeField] GameObject _animationRugEnd = default!;

    [SerializeField] PlayableDirector _directorRugScrollOnStart = default!;
    [SerializeField] PlayableDirector _directorRugScrollOnEnd = default!;

    /// <summary>
    /// アニメーション再生中か
    /// </summary>
    private bool _isPlaying = false;

    public async UniTask PlayStartAnimation()
    {
        _animationRugStart.SetActive(true);
        _animationRugEnd.SetActive(false);

        //タイムラインの再生
        _directorRugScrollOnStart.Play();

        OnAnimationStart();

        var token = this.GetCancellationTokenOnDestroy();

        while (_isPlaying)
        {
            await UniTask.Yield(token);
        }
    }

    public async UniTask PlayEndAnimation()
    {
        _animationRugStart.SetActive(false);
        _animationRugEnd.SetActive(true);

        //タイムラインの再生
        _directorRugScrollOnEnd.Play();

        OnAnimationStart();

        var token = this.GetCancellationTokenOnDestroy();

        while (_isPlaying)
        {
            await UniTask.Yield(token);
        }
    }

    private void OnAnimationStart()
    {
        _animationRugCanvas.enabled = true;
        _normalRugCanvas.enabled = false;

        _isPlaying = true;
    }

    //タイムラインの最終フレームで実行
    public void OnStartAnimationComplete()
    {
        _animationRugCanvas.enabled = false;
        _normalRugCanvas.enabled = true;

        _isPlaying = false;
    }    
    
    //タイムラインの最終フレームで実行
    public void OnEndAnimationComplete()
    {
        _animationRugCanvas.enabled = false;
        _normalRugCanvas.enabled = false;

        _isPlaying = false;
    }
}
