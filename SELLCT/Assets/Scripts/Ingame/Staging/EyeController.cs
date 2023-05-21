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
    [Header("Default�̉𑜓x��1�{�Ƃ��āA�ȉ��̒l�ŏ㏸���܂��B\n��F�l��2�̏ꍇ 0.5��1")]
    [SerializeField, Range(1f, 2160f)] float _eyeIncreaseValue;
    [Header("Default�̉𑜓x��1�{�Ƃ��āA�ȉ��̒l�Ō������܂��B\n��F�l��0.7�̏ꍇ 1��0.7")]
    [SerializeField, Range(0.0001f, 1f)] float _eyeDecreaseValue;

    [Header("�����̉𑜓x��ݒ肵�܂��B\n�܂��A�����̃G�������g�������Ɋ֌W�Ȃ����̒l�ɂȂ�܂��B")]
    [SerializeField, Range(0, 2160f)] float _firstEye = 1080f;
    [Header("���o�p��Image��ݒ肵�܂�")]
    [SerializeField] GameObject stagingImage;

    float _currentEyeValue = 1.0f;
    const float MAX_VALUE = 2160f;
    const float MIN_VALUE = 1;

    private void Awake()
    {
        //�ŏ��̏��������Ɗ֌W�Ȃ��ݒ肳���B�t�F�[�Y�Ɗ֌W�Ȃ��ݒ肷�邽�߁A��肪��������ύX����
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
