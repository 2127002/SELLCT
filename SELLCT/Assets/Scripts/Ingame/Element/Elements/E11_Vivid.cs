using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class E11_Vivid : Card
{
    [SerializeField] Material _vivid = default!;
    [Header("Default�̍ʓx��100%�Ƃ��āA�ȉ��̒l�ŏ㏸���܂��B\n��F�l��10�̏ꍇ 100%��110%")]
    [SerializeField, Range(0, 150f)] float _vividIncreasePercent;
    [Header("Default�̍ʓx��100%�Ƃ��āA�ȉ��̒l�Ō������܂��B\n��F�l��4.5�̏ꍇ 55%��50.5%")]
    [SerializeField, Range(0, 150f)] float _vividDecreasePercent;

    [Header("�����̍ʓx��ݒ肵�܂��BDefault�̍ʓx��100%�ł��B\n�܂��A�����̃G�������g�������Ɋ֌W�Ȃ����̒l�ɂȂ�܂��B")]
    [SerializeField, Range(0, 150f)] float _firstVividPercent = 100f;

    float _currentVividValue = 1.0f;
    const float MAX_VALUE = 1.5f;
    const float MIN_VALUE = 0;

    public override int Id => 11;

    private void Awake()
    {
        //�ŏ��̏��������Ɗ֌W�Ȃ��ݒ肳���B�t�F�[�Y�Ɗ֌W�Ȃ��ݒ肷�邽�߁A��肪��������ύX����
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
    //E11�����̃N���X�ł��BVivid�̕ύX�́APlayMode�O�Ɉ����p����邽�߃��Z�b�g����{�^����p�ӂ��܂��B
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
