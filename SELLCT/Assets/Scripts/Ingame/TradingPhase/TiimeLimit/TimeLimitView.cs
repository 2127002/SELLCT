using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TimeLimitView : MonoBehaviour
{
    [SerializeField] RectTransform _clockHandTransform = default!;

    float _scaleRate = 0.7f; // �X�P�[���ύX��
    Vector3 _originalScale = Vector3.one; // ���̃X�P�[��

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
    /// ���v�̐j���w�莞�ԕ���
    /// </summary>
    /// <param name="maxTimeLimit">�w�W�ƂȂ�ő吧������</param>
    /// <param name="elapsedTime">�o�ߎ���</param>
    public void Rotate(float maxTimeLimit, float currentTimeLimit)
    {
        float rotationSpeed = 360f / maxTimeLimit;
        if (Mathf.Approximately(maxTimeLimit, 0f)) { return; }

        _clockHandTransform.localEulerAngles = new(0f, 0f, currentTimeLimit * rotationSpeed);
    }

    /// <summary>
    /// ���v�̐j���X�P�[���𒲐�����
    /// </summary>
    /// <param name="maxTimeLimit">�w�W�ƂȂ�ő吧������</param>
    /// <param name="elapsedTime">�o�ߎ���</param>
    public void Scale(float maxTimeLimit, float currentTimeLimit)
    {
        // �����ň�U�o�ߎ��Ԃ����Z�b�g����
        float TimeOfHalfLap = maxTimeLimit / 2f;
        float timeElapsed = currentTimeLimit % TimeOfHalfLap;

        //�s���ƋA��ŃX�P�[�����قȂ�
        _scaleRate = currentTimeLimit > TimeOfHalfLap ? 0.7f : 0.8f;

        // �X�P�[����ύX����
        float rate = timeElapsed / maxTimeLimit;
        float sinRate = 1f - Mathf.Sin(rate * Mathf.PI * 2f);
        float scale = Mathf.Lerp(_originalScale.magnitude * _scaleRate, _originalScale.magnitude, sinRate);
        _clockHandTransform.localScale = _originalScale.normalized * scale;
    }
}
