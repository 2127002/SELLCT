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
        //���݂͉��ł����ɔz�u���Ă���B�{���̓X�^�[�g��b�I�����Ɏ��s
        AnimationStart();
    }

    public void AnimationStart()
    {
        //�^�C�����C���̍Đ�
        _directorRugScroll.Play();

        OnAnimationStart();
    }

    private void OnAnimationStart()
    {
        _animationRugCanvas.enabled = true;
        _normalRugCanvas.enabled = false;
    }

    //�^�C�����C���̍ŏI�t���[���Ŏ��s
    public void OnAnimationComplete()
    {
        _animationRugCanvas.enabled = false;
        _normalRugCanvas.enabled = true;
    }
}
