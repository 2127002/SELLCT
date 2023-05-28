using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TimeLimitView : MonoBehaviour
{
    [SerializeField] RectTransform _clockHandTransform = default!;

    float _scaleRate = 0.7f; // スケール変更率
    Vector3 _originalScale = Vector3.one; // 元のスケール

    private void Awake()
    {
        _originalScale = _clockHandTransform.localScale;
    }

    private void OnEnable()
    {
        _clockHandTransform.localScale = _originalScale;
        _clockHandTransform.localRotation = Quaternion.identity;
    }

    /// <summary>
    /// 時計の針を指定時間分回す
    /// </summary>
    /// <param name="maxTimeLimit">指標となる最大制限時間</param>
    /// <param name="elapsedTime">経過時間</param>
    public void Rotate(float maxTimeLimit, float currentTimeLimit)
    {
        float rotationSpeed = 360f / maxTimeLimit;
        if (Mathf.Approximately(maxTimeLimit, 0f)) { return; }

        _clockHandTransform.localEulerAngles = new(0f, 0f, currentTimeLimit * rotationSpeed);
    }

    /// <summary>
    /// 時計の針をスケールを調整する
    /// </summary>
    /// <param name="maxTimeLimit">指標となる最大制限時間</param>
    /// <param name="elapsedTime">経過時間</param>
    public void Scale(float maxTimeLimit, float currentTimeLimit)
    {
        // 半周で一旦経過時間をリセットする
        float TimeOfHalfLap = maxTimeLimit / 2f;
        float timeElapsed = currentTimeLimit % TimeOfHalfLap;

        //行きと帰りでスケールが異なる
        _scaleRate = currentTimeLimit > TimeOfHalfLap ? 0.7f : 0.8f;

        // スケールを変更する
        float rate = timeElapsed / maxTimeLimit;
        float sinRate = 1f - Mathf.Sin(rate * Mathf.PI * 2f);
        float scale = Mathf.Lerp(_originalScale.magnitude * _scaleRate, _originalScale.magnitude, sinRate);
        _clockHandTransform.localScale = _originalScale.normalized * scale;
    }
}
