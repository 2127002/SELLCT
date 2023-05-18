using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagingHund : MonoBehaviour
{
    [SerializeField] Material mat_noi;
    [SerializeField] Material mat_br;
    [SerializeField] Material mat_pi;
    [SerializeField] Material mat_scRot;
    [Header("��ʌ��ʂ������鑬�x")]
    [SerializeField] float raitoSpd;
    [Header("��ʌ��ʂ̉�]���x")]
    [SerializeField] float rotateSpd;
    [Header("��ʌ��ʂ̃m�C�Y�̍ő�̑傫��")]
    [SerializeField] float MaxNoizeScale;
    [Header("��ʌ��ʂ̃m�C�Y�̑傫���̕ω����[�g")]
    [SerializeField] float NoizeScaleRate;
    [Header("��ʂ̉𑜓x")]
    [SerializeField] float pi = 200;
    float br = 0;
    float rotate = 0f;
    float noizeScale = 1f;
    float ratio = 1f;
    float scrennRotate = 90f;

    private void Start()
    {
        mat_noi.SetFloat("_Rotate", rotate);
        mat_noi.SetFloat("_NoiseScale", noizeScale);
        mat_noi.SetFloat("_Ratio", ratio);
        mat_scRot.SetFloat("_ScreenRotate", 0);
    }

    public void SignalScriptTest()
    {
        Debug.Log("���\�b�h�����s���܂����B");
    }

    public void ChangeRotate()
    {
        rotate = rotateSpd;
        mat_noi.SetFloat("_Rotate", rotate);
        Debug.Log("Rotate�̒l��ύX���܂���");

    }
    public void ChangeNoizeScale()
    {
        StartCoroutine("IChangeNoizeScale");
        Debug.Log("NoizeScale�̒l��ύX���܂���");
    }

    public void ChangeRatiosub()
    {
        StartCoroutine("IChangeRatiosub");
        Debug.Log("Ratio�̒l��ύX���܂���");
    }

    public void ChangeScreenRotate()
    {
        mat_scRot.SetFloat("_ScreenRotate", scrennRotate);
        Debug.Log("ScreenRotate�̒l��ύX���܂���");
    }

    public void ChangeRatioplus()
    {
        StartCoroutine("IChangeRatioplus");
        Debug.Log("Ratio�̒l��ύX���܂���");
    }

    public void ChangeBrigthness()
    {
        mat_br.SetFloat("_Brighness", br);
        Debug.Log("���x�̒l��ύX���܂���");
    }

    public void ChangeResolution()
    {
        mat_pi.SetFloat("_Resolution", pi);
        Debug.Log("�𑜓x�̒l��ύX���܂���");
    }

    IEnumerator IChangeRatiosub()
    {
        while (true)
        {
            if (ratio <= 0f) yield break;
            ratio -= raitoSpd * Time.deltaTime;
            mat_noi.SetFloat("_Ratio", ratio);
            yield return null;
        }
    }

    IEnumerator IChangeRatioplus()
    {
        while (true)
        {

            if (ratio >= 1)
            {
                ratio = 1;
                mat_noi.SetFloat("_Ratio", ratio);
                yield break;
            }
            ratio += raitoSpd * Time.deltaTime;
            mat_noi.SetFloat("_Ratio", ratio);
            yield return null;
        }
    }

    IEnumerator IChangeNoizeScale()
    {
        while (true)
        {
            noizeScale = Mathf.PingPong(Time.time * NoizeScaleRate, MaxNoizeScale) + 1f;
            mat_noi.SetFloat("_NoiseScale", noizeScale);
            yield return null;
        }
    }
}
