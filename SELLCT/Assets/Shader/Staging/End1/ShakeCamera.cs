using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    [SerializeField] Transform cameraTransform; // カメラのTransformコンポーネント
    [SerializeField] float shakeDuration; // カメラのTransformコンポーネント

    private Vector3 originalPosition; // カメラの初期位置

    void Awake()
    {
        if (cameraTransform == null)
        {
            cameraTransform = GetComponent<Transform>();
        }

        originalPosition = cameraTransform.localPosition; // カメラの初期位置を保存
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
            // 揺れのランダムな位置を生成
            float offsetX = Random.Range(-1f, 1f) * shakeMagnitude;
            float offsetY = Random.Range(-1f, 1f) * shakeMagnitude;

            // カメラの位置を更新
            cameraTransform.localPosition = new Vector3(originalPosition.x + offsetX, originalPosition.y + offsetY, originalPosition.z);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 揺れが終了したらカメラを元の位置に戻す
        cameraTransform.localPosition = originalPosition;
    }
}
