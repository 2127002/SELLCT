using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLimitView : MonoBehaviour
{
    [SerializeField] RectTransform _clockHandTransform = default!;

  const  float scaleRate = 0.7f; // スケール変更率
    Vector3 originalScale = Vector3.one; // 元のスケール
    float timeElapsed = 0; // 経過時間

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
        // 経過時間を加算
        timeElapsed += Time.deltaTime;

        // 1周したら経過時間をリセット
        if (timeElapsed > maxTimeLimit)
        {
            timeElapsed = 0.0f;
        }

        // スケールを変更する
        float rate = timeElapsed / maxTimeLimit;
        float sinRate = 1f - Mathf.Sin(rate * Mathf.PI * 2.0f);
        float scale = Mathf.Lerp(originalScale.magnitude * scaleRate, originalScale.magnitude, sinRate);
        _clockHandTransform.localScale = originalScale.normalized * scale;
    }
}
