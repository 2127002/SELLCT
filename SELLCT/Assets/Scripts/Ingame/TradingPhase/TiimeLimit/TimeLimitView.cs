using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLimitView : MonoBehaviour
{
    [SerializeField] RectTransform _clockHandTransform = default!;

    float _scaleRate = 0.8f; // スケール変更率
    Vector3 _originalScale = Vector3.one; // 元のスケール
    float _timeElapsed = 0; // 経過時間

    private void Awake()
    {
        _originalScale = _clockHandTransform.localScale;
    }

    private void OnEnable()
    {
        _clockHandTransform.localScale = _originalScale;
        _clockHandTransform.localRotation = Quaternion.identity;
        _timeElapsed = 0;
    }

    public void Rotate(float maxTimeLimit)
    {
        float rotationSpeed = 360f / maxTimeLimit;

        _clockHandTransform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    public void Scale(float maxTimeLimit)
    {
        // 経過時間を加算
        _timeElapsed += Time.deltaTime;

        // 半周で一旦経過時間をリセットする
        if (_timeElapsed > maxTimeLimit / 2)
        {
            _timeElapsed = 0.0f;

            //行きと帰りでスケールが異なる
            _scaleRate = 0.7f;
        }

        // スケールを変更する
        float rate = _timeElapsed / maxTimeLimit;
        float sinRate = 1f - Mathf.Sin(rate * Mathf.PI * 2.0f);
        float scale = Mathf.Lerp(_originalScale.magnitude * _scaleRate, _originalScale.magnitude, sinRate);
        _clockHandTransform.localScale = _originalScale.normalized * scale;
    }
}
