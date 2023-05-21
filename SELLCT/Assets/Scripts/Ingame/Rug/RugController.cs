using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class RugController : MonoBehaviour
{
    [SerializeField] Canvas _normalRugCanvas = default!;
    [SerializeField] Canvas _animationRugCanvas = default!;

    [SerializeField] PlayableDirector _directorRugScroll = default!;

    private void Start()
    {
        //現在は仮でここに配置している。本来はスタート会話終了時に実行
        AnimationStart();
    }

    public void AnimationStart()
    {
        //タイムラインの再生
        _directorRugScroll.Play();

        OnAnimationStart();
    }

    private void OnAnimationStart()
    {
        _animationRugCanvas.enabled = true;
        _normalRugCanvas.enabled = false;
    }

    //タイムラインの最終フレームで実行
    public void OnAnimationComplete()
    {
        _animationRugCanvas.enabled = false;
        _normalRugCanvas.enabled = true;
    }
}
