using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.PackageManager.UI;
#endif
using UnityEngine;

public class E10_Light : Card
{
    [SerializeField] CardParameter _parameter = default!;
    [SerializeField] MoneyPossessedController _controller = default!;
    [SerializeField] Sprite _baseSprite = default!;
    [SerializeField] Sprite _number = default!;
    [SerializeField] Sprite _chineseCharacters = default!;
    [SerializeField] Sprite _hiragana = default!;
    [SerializeField] Sprite _katakana = default!;
    [SerializeField] Sprite _alphabet = default!;
    [SerializeField] HandMediator _handMediator = default!;

    [SerializeField] Material _brightness = default!;
    [Header("Default�̖��邳��100%�Ƃ��āA�ȉ��̒l�ŏ㏸���܂��B\n��F�l��10�̏ꍇ 100%��110%")]
    [SerializeField, Range(0, 150f)] float _brightnessIncreasePercent;
    [Header("Default�̖��邳��100%�Ƃ��āA�ȉ��̒l�Ō������܂��B\n��F�l��4.5�̏ꍇ 55%��50.5%")]
    [SerializeField, Range(0, 150f)] float _brightnessDecreasePercent;

    [Header("�����̖��x��ݒ肵�܂��BDefault�̖��邳��100%�ł��B\n�܂��A�����̃G�������g�������Ɋ֌W�Ȃ����̒l�ɂȂ�܂��B")]
    [SerializeField, Range(0, 150f)] float _firstBrightnessPercent = 100f;

    float _currentBrightnessValue = 1.0f;
    const float MAX_VALUE = 1.5f;
    const float MIN_VALUE = 0;

    readonly List<Sprite> result = new();

    public override string CardName => _parameter.GetName();
    public override bool IsDisposedOfAfterSell => _parameter.IsDisposedOfAfterSell();
    public override int Rarity => _parameter.Rarity();
    public override IReadOnlyList<Sprite> CardSprite
    {
        get
        {
            //������
            if (result.Count == 0)
            {
                result.Add(_baseSprite);
                result.Add(_number);
                result.Add(_chineseCharacters);
                result.Add(_hiragana);
                result.Add(_katakana);
                result.Add(_alphabet);
            }

            return result;
        }
    }
    public override bool ContainsPlayerDeck => _handMediator.ContainsCard(this);
    public int FindAll => _handMediator.FindAll(this);

    private void Awake()
    {
        //�ŏ��̏��������Ɗ֌W�Ȃ��ݒ肳���B�t�F�[�Y�Ɗ֌W�Ȃ��ݒ肷�邽�߁A��肪��������ύX����
        _currentBrightnessValue = _firstBrightnessPercent / 100f;
        SetBrightness();
    }

    public override void Buy()
    {
        _controller.DecreaseMoney(_parameter.GetMoney());

        IncreaseBrightnessValue();
    }

    public override void Passive()
    {
        // DoNothing
    }

    public override void Sell()
    {
        _controller.IncreaseMoney(_parameter.GetMoney());

        DesreaseBrightnessValue();
    }

    private void IncreaseBrightnessValue()
    {
        _currentBrightnessValue = Mathf.Min(MAX_VALUE, _currentBrightnessValue + (_brightnessIncreasePercent / 100f));

        SetBrightness();
    }

    private void DesreaseBrightnessValue()
    {
        _currentBrightnessValue = Mathf.Max(MIN_VALUE, _currentBrightnessValue - (_brightnessDecreasePercent / 100f));

        SetBrightness();
    }

    private void SetBrightness()
    {
        _brightness.SetFloat("_Brightness", _currentBrightnessValue);
    }


#if UNITY_EDITOR
    //E10�����̃N���X�ł��BBrightness�̕ύX�́APlayMode�O�Ɉ����p����邽�߃��Z�b�g����{�^����p�ӂ��܂��B
    class BrightnessResetWindow : EditorWindow
    {
        private E10_Light _light;
        private float _brightnessValue = 100f;
        public void SetLight(E10_Light light)
        {
            _light = light;
        }

        [MenuItem("Window/Util/Brightness Window")]
        public static void ShowWindow()
        {
            BrightnessResetWindow window = GetWindow<BrightnessResetWindow>("Brightness Changer");
            E10_Light light = FindObjectOfType<E10_Light>();
            window.SetLight(light);
        }

        private void OnGUI()
        {
            if (_light != null)
            {
                if (GUILayout.Button("Reset"))
                {
                    _light._brightness.SetFloat("_Brightness", 1f);
                    _brightnessValue = 100f;
                }
                GUILayout.Space(10);
                if (GUILayout.Button("Set Brightness"))
                {
                    _light._brightness.SetFloat("_Brightness", _brightnessValue / 100f);
                }

                _brightnessValue = EditorGUILayout.Slider("Brightness Value", _brightnessValue, 0f, 150f);
            }
            else
            {
                EditorGUILayout.HelpBox("E10_Light object not found in scene.", MessageType.Warning);
                if (GUILayout.Button("Reload"))
                {
                    BrightnessResetWindow window = GetWindow<BrightnessResetWindow>("Brightness Changer");
                    E10_Light light = FindObjectOfType<E10_Light>();
                    window.SetLight(light);
                }
            }
        }
    }
#endif
}