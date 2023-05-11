using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagingHund : MonoBehaviour
{
    [SerializeField] Material mat;
    [SerializeField] Material mat_br;
    [SerializeField] Material mat_pi;
    [Header("画面効果がかかる速度")]
    [SerializeField] float raitoSpd;
    [Header("画面効果の回転速度")]
    [SerializeField] float rotateSpd;
    [Header("画面効果のノイズの最大の大きさ")]
    [SerializeField] float MaxNoizeScale;
    [Header("画面効果のノイズの大きさの変化レート")]
    [SerializeField] float NoizeScaleRate;
    [Header("画面の解像度")]
    [SerializeField] float pi = 200;
    float br = 0;
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
        Debug.Log("メソッドを実行しました。");
    }

    public void ChangeRotate()
    {
        rotate = rotateSpd;
        mat.SetFloat("_Rotate", rotate);
        Debug.Log("Rotateの値を変更しました");

    }
    public void ChangeNoizeScale()
    {
        StartCoroutine("IChangeNoizeScale");
        Debug.Log("NoizeScaleの値を変更しました");
    }    
    
    public void ChangeRatiosub()
    {
        StartCoroutine("IChangeRatiosub");
        Debug.Log("Ratioの値を変更しました");
    } 
    
    public void ChangeRatioplus()
    {
        StartCoroutine("IChangeRatioplus");
        Debug.Log("Ratioの値を変更しました");
    }
    
    public void ChangeBrigthness()
    {
        mat_br.SetFloat("_Brighness", br);
        Debug.Log("明度の値を変更しました");
    }

    public void ChangeResolution()
    {
        mat_pi.SetFloat("_Resolution", pi);
        Debug.Log("解像度の値を変更しました");
    }

    IEnumerator IChangeRatiosub()
    {
        while(true)
        {
            if (ratio <= 0f) yield break;
            ratio -= raitoSpd * Time.deltaTime;
            mat.SetFloat("_Ratio", ratio);
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
                mat.SetFloat("_Ratio", ratio);
                yield break;
            }
            ratio += raitoSpd * Time.deltaTime;
            mat.SetFloat("_Ratio", ratio);
            yield return null;
        }
    }

    IEnumerator IChangeNoizeScale()
    {
        while (true)
        {
            noizeScale = Mathf.PingPong(Time.time * NoizeScaleRate, MaxNoizeScale) + 1f;
            mat.SetFloat("_NoiseScale", noizeScale);
            yield return null;
        }
    }
}
