using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLimitView : MonoBehaviour
{
    [SerializeField] RectTransform _clockHandTransform = default!;

    float _scaleRate = 0.8f; // �X�P�[���ύX��
    Vector3 _originalScale = Vector3.one; // ���̃X�P�[��
    float _timeElapsed = 0; // �o�ߎ���

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
        // �o�ߎ��Ԃ����Z
        _timeElapsed += Time.deltaTime;

        // �����ň�U�o�ߎ��Ԃ����Z�b�g����
        if (_timeElapsed > maxTimeLimit / 2)
        {
            _timeElapsed = 0.0f;

            //�s���ƋA��ŃX�P�[�����قȂ�
            _scaleRate = 0.7f;
        }

        // �X�P�[����ύX����
        float rate = _timeElapsed / maxTimeLimit;
        float sinRate = 1f - Mathf.Sin(rate * Mathf.PI * 2.0f);
        float scale = Mathf.Lerp(_originalScale.magnitude * _scaleRate, _originalScale.magnitude, sinRate);
        _clockHandTransform.localScale = _originalScale.normalized * scale;
    }
}
