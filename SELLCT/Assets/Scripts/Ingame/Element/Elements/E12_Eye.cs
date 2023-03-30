using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class E12_Eye : Card
{
    [SerializeField] Material _eye = default!;
    [Header("Defaultの解像度を1倍として、以下の値で上昇します。\n例：値が2の場合 0.5→1")]
    [SerializeField, Range(1f, 2160f)] float _eyeIncreaseValue;
    [Header("Defaultの解像度を1倍として、以下の値で減少します。\n例：値が0.7の場合 1→0.7")]
    [SerializeField, Range(0.0001f, 1f)] float _eyeDecreaseValue;

    [Header("初期の解像度を設定します。\nまた、初期のエレメント所持数に関係なくこの値になります。")]
    [SerializeField, Range(0, 2160f)] float _firstEye = 1080f;

    float _currentEyeValue = 1.0f;
    const float MAX_VALUE = 2160f;
    const float MIN_VALUE = 1;

    public override int Id => 12;

    private void Awake()
    {
        //最初の所持枚数と関係なく設定される。フェーズと関係なく設定するため、問題が生じたら変更推奨
        _currentEyeValue = _firstEye;
        SetEye();
    }

    public override void Buy()
    {
        _moneyPossessedCcontroller.DecreaseMoney(_parameter.GetMoney());

        IncreaseEyeValue();
    }

    public override void OnPressedU6Button()
    {
        throw new System.NotImplementedException();
    }

    public override void Sell()
    {
        _moneyPossessedCcontroller.IncreaseMoney(_parameter.GetMoney());

        DesreaseEyeValue();
    }

    private void IncreaseEyeValue()
    {
        _currentEyeValue = Mathf.Min(MAX_VALUE, _currentEyeValue * _eyeIncreaseValue);

        SetEye();
    }

    private void DesreaseEyeValue()
    {
        _currentEyeValue = Mathf.Max(MIN_VALUE, _currentEyeValue * _eyeDecreaseValue);

        SetEye();
    }

    private void SetEye()
    {
        _eye.SetFloat("_Resolution", _currentEyeValue);
    }


#if UNITY_EDITOR
    class EyeResetWindow : EditorWindow
    {
        private E12_Eye _eye;
        private float _eyeValue = 1080f;
        public void SetEye(E12_Eye eye)
        {
            _eye = eye;
        }

        [MenuItem("Window/Util/Eye Window")]
        public static void ShowWindow()
        {
            EyeResetWindow window = GetWindow<EyeResetWindow>("Eye Changer");
            E12_Eye eye = FindObjectOfType<E12_Eye>();
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
                EditorGUILayout.HelpBox("E12_Eye object not found in scene.", MessageType.Warning);
                if (GUILayout.Button("Reload"))
                {
                    ShowWindow();
                }
            }
        }
    }
#endif

}
