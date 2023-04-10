using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class E11_Vivid : Card
{
    [SerializeField] Material _vivid = default!;
    [Header("Defaultの彩度を100%として、以下の値で上昇します。\n例：値が10の場合 100%→110%")]
    [SerializeField, Range(0, 150f)] float _vividIncreasePercent;
    [Header("Defaultの彩度を100%として、以下の値で減少します。\n例：値が4.5の場合 55%→50.5%")]
    [SerializeField, Range(0, 150f)] float _vividDecreasePercent;

    [Header("初期の彩度を設定します。Defaultの彩度は100%です。\nまた、初期のエレメント所持数に関係なくこの値になります。")]
    [SerializeField, Range(0, 150f)] float _firstVividPercent = 100f;

    float _currentVividValue = 1.0f;
    const float MAX_VALUE = 1.5f;
    const float MIN_VALUE = 0;

    public override int Id => 11;

    private void Awake()
    {
        //最初の所持枚数と関係なく設定される。フェーズと関係なく設定するため、問題が生じたら変更推奨
        _currentVividValue = _firstVividPercent / 100f;
        SetVivid();
    }

    public override void Buy()
    {
        IncreaseVividValue();
    }

    public override void OnPressedU6Button()
    {
        throw new System.NotImplementedException();
    }

    public override void Sell()
    {
        DesreaseVividValue();
    }

    private void IncreaseVividValue()
    {
        _currentVividValue = Mathf.Min(MAX_VALUE, _currentVividValue + (_vividIncreasePercent / 100f));

        SetVivid();
    }

    private void DesreaseVividValue()
    {
        _currentVividValue = Mathf.Max(MIN_VALUE, _currentVividValue - (_vividDecreasePercent / 100f));

        SetVivid();
    }

    private void SetVivid()
    {
        _vivid.SetFloat("_Saturation", _currentVividValue);
    }


#if UNITY_EDITOR
    //E11内部のクラスです。Vividの変更は、PlayMode外に引き継がれるためリセットするボタンを用意します。
    class VividResetWindow : EditorWindow
    {
        private E11_Vivid _vivid;
        private float _vividValue = 100f;
        public void SetVivid(E11_Vivid vivid)
        {
            _vivid = vivid;
        }

        [MenuItem("Window/Util/Vivid Window")]
        public static void ShowWindow()
        {
            VividResetWindow window = GetWindow<VividResetWindow>("Vivid Changer");
            E11_Vivid vivid = FindObjectOfType<E11_Vivid>();
            window.SetVivid(vivid);
        }

        private void OnGUI()
        {
            if (_vivid != null)
            {
                if (GUILayout.Button("Reset"))
                {
                    _vivid._vivid.SetFloat("_Saturation", 1f);
                    _vividValue = 100f;
                }
                GUILayout.Space(10);
                if (GUILayout.Button("Set Vivid"))
                {
                    _vivid._vivid.SetFloat("_Saturation", _vividValue / 100f);
                }

                _vividValue = EditorGUILayout.Slider("Vivid Value", _vividValue, 0f, 150f);
            }
            else
            {
                EditorGUILayout.HelpBox("E11_Vivid object not found in scene.", MessageType.Warning);
                if (GUILayout.Button("Reload"))
                {
                    ShowWindow();
                }
            }
        }
    }
#endif
}
