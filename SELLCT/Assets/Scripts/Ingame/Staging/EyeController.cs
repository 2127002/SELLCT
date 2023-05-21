using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;


public class EyeController : MonoBehaviour
{
    [SerializeField] Material _eye = default!;
    [Header("Defaultの解像度を1倍として、以下の値で上昇します。\n例：値が2の場合 0.5→1")]
    [SerializeField, Range(1f, 2160f)] float _eyeIncreaseValue;
    [Header("Defaultの解像度を1倍として、以下の値で減少します。\n例：値が0.7の場合 1→0.7")]
    [SerializeField, Range(0.0001f, 1f)] float _eyeDecreaseValue;

    [Header("初期の解像度を設定します。\nまた、初期のエレメント所持数に関係なくこの値になります。")]
    [SerializeField, Range(0, 2160f)] float _firstEye = 1080f;
    [Header("演出用のImageを設定します")]
    [SerializeField] GameObject stagingImage;

    float _currentEyeValue = 1.0f;
    const float MAX_VALUE = 2160f;
    const float MIN_VALUE = 1;

    private void Awake()
    {
        //最初の所持枚数と関係なく設定される。フェーズと関係なく設定するため、問題が生じたら変更推奨
        _currentEyeValue = _firstEye;
        SetEye();
    }

    public void IncreaseEyeValue()
    {
        _currentEyeValue = Mathf.Min(MAX_VALUE, _currentEyeValue * _eyeIncreaseValue);

        SetEye();
    }

    public void DesreaseEyeValue()
    {
        _currentEyeValue = Mathf.Max(MIN_VALUE, _currentEyeValue * _eyeDecreaseValue);

        SetEye();
    }

    public void DeactiveImage()
    {
        stagingImage.SetActive(false);
    }

    private void SetEye()
    {
        _eye.SetFloat("_Resolution", _currentEyeValue);
    }

#if UNITY_EDITOR
    class EyeResetWindow : EditorWindow
    {
        private EyeController _eye;
        private float _eyeValue = 1080f;
        public void SetEye(EyeController eye)
        {
            _eye = eye;
        }

        [MenuItem("Window/Util/Eye Window")]
        public static void ShowWindow()
        {
            EyeResetWindow window = GetWindow<EyeResetWindow>("Eye Changer");
            EyeController eye = FindObjectOfType<EyeController>();
            window.SetEye(eye);
        }

        private void OnGUI()
        {
            if (_eye != null)
            {
                if (GUILayout.Button("Reset"))
                {
                    _eye._eye.SetFloat("_Resolution", 1080f);
                    _eyeValue = 1080f;
                }
                GUILayout.Space(10);
                if (GUILayout.Button("Set EyeValue"))
                {
                    _eye._eye.SetFloat("_Resolution", _eyeValue);
                }

                _eyeValue = EditorGUILayout.Slider("Eye Value", _eyeValue, 1f, 2160f);
            }
            else
            {
                EditorGUILayout.HelpBox("EyeController object not found in scene.", MessageType.Warning);
                if (GUILayout.Button("Reload"))
                {
                    ShowWindow();
                }
            }
        }
    }
#endif

}
