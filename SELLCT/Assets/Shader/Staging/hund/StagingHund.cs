using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagingHund : MonoBehaviour
{
    [SerializeField] Material mat;
    [Header("��ʌ��ʂ������鑬�x")]
    [SerializeField] float raitoSpd;
    [Header("��ʌ��ʂ̉�]���x")]
    [SerializeField] float rotateSpd;
    [Header("��ʌ��ʂ̃m�C�Y�̍ő�̑傫��")]
    [SerializeField] float MaxNoizeScale;
    [Header("��ʌ��ʂ̃m�C�Y�̑傫���̕ω����[�g")]
    [SerializeField] float NoizeScaleRate;

    float rotate = 0f;
    float noizeScale = 1f;
    float ratio = 1f;

    private void Start()
    {
        mat.SetFloat("_Rotate", rotate);
        mat.SetFloat("_NoiseScale", noizeScale);
        mat.SetFloat("_Ratio", ratio);
    }

    public void SignalScriptTest()
    {
        Debug.Log("���\�b�h�����s���܂����B");
    }

    public void ChangeRotate()
    {
        rotate = rotateSpd;
        mat.SetFloat("_Rotate", rotate);
        Debug.Log("Rotate�̒l��ύX���܂���");

    }
    public void ChangeNoizeScale()
    {
        StartCoroutine("IChangeNoizeScale");
        Debug.Log("NoizeScale�̒l��ύX���܂���");
    }    
    
    public void ChangeRatio()
    {
        StartCoroutine("IChangeRatio");
        Debug.Log("Ratio�̒l��ύX���܂���");
    }

    IEnumerator IChangeRatio()
    {
        while(true)
        {
            if (ratio <= 0f) yield break;
            ratio -= raitoSpd * Time.deltaTime;
            mat.SetFloat("_Ratio", ratio);
            yield return null;
        }
    }

    IEnumerator IChangeNoizeScale()
    {
        while (true)
        {
            //if (noizeScale >= 10f) yield break;
            noizeScale = Mathf.PingPong(Time.time * NoizeScaleRate, MaxNoizeScale) + 1f;
            mat.SetFloat("_NoiseScale", noizeScale);
            yield return null;
        }
    }
}
