using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    [SerializeField] Transform cameraTransform; // �J������Transform�R���|�[�l���g
    [SerializeField] float shakeDuration; // �J������Transform�R���|�[�l���g

    private Vector3 originalPosition; // �J�����̏����ʒu

    void Awake()
    {
        if (cameraTransform == null)
        {
            cameraTransform = GetComponent<Transform>();
        }

        originalPosition = cameraTransform.localPosition; // �J�����̏����ʒu��ۑ�
    }

    public void StartShake( float shakeMagnitude)
    {
        StartCoroutine(Shake(shakeMagnitude));
    }
    IEnumerator Shake(float shakeMagnitude)
    {
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            // �h��̃����_���Ȉʒu�𐶐�
            float offsetX = Random.Range(-1f, 1f) * shakeMagnitude;
            float offsetY = Random.Range(-1f, 1f) * shakeMagnitude;

            // �J�����̈ʒu���X�V
            cameraTransform.localPosition = new Vector3(originalPosition.x + offsetX, originalPosition.y + offsetY, originalPosition.z);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // �h�ꂪ�I��������J���������̈ʒu�ɖ߂�
        cameraTransform.localPosition = originalPosition;
    }
}
