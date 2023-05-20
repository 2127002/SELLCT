using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessController : MonoBehaviour
{
    [SerializeField]
    float intensity;
    [SerializeField]
    Volume postFXvolume;
    [SerializeField]
    private Animator _vinetteAnim;

    private bool _isStartVignette = true;
    private Vignette _vignette;

    void Start()
    {
        postFXvolume.profile.TryGet(out _vignette);
        if (_vignette == null) return;
        
    }

    void Update()
    {
        _vignette.intensity.value = intensity;
        ////�|�X�g�G�t�F�N�g�ǉ�
        //if (Input.GetKeyDown(KeyCode.X))
        //{
        //    ChromaticAberration w = m_Volume.profile.Add<ChromaticAberration>(true);
        //    w.intensity.value = 100;//�ǉ����Ēl��ύX
        //}
    }

    void OnVignette()
    {
        _vinetteAnim.SetBool("IsVignette",_isStartVignette );
    }
    

}