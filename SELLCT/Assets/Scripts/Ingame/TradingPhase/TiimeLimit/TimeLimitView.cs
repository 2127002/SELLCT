using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLimitView : MonoBehaviour
{
    [SerializeField] RectTransform _clockHandTransform = default!;

  const  float scaleRate = 0.7f; // �X�P�[���ύX��
    Vector3 originalScale = Vector3.one; // ���̃X�P�[��
    float timeElapsed = 0; // �o�ߎ���

    private void Awake()
    {
        originalScale = _clockHandTransform.localScale;
    }

    public  void Rotate(float maxTimeLimit)
    {
        float rotationSpeed = 360f / maxTimeLimit;

        _clockHandTransform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    public void Scale(float maxTimeLimit)
    {
        // �o�ߎ��Ԃ����Z
        timeElapsed += Time.deltaTime;

        // 1��������o�ߎ��Ԃ����Z�b�g
        if (timeElapsed > maxTimeLimit)
        {
            timeElapsed = 0.0f;
        }

        // �X�P�[����ύX����
        float rate = timeElapsed / maxTimeLimit;
        float sinRate = 1f - Mathf.Sin(rate * Mathf.PI * 2.0f);
        float scale = Mathf.Lerp(originalScale.magnitude * scaleRate, originalScale.magnitude, sinRate);
        _clockHandTransform.localScale = originalScale.normalized * scale;
    }
}
