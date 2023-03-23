using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class E11_Vivid : Card
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

    private void Awake()
    {
        //�ŏ��̏��������Ɗ֌W�Ȃ��ݒ肳���B�t�F�[�Y�Ɗ֌W�Ȃ��ݒ肷�邽�߁A��肪��������ύX����
        _currentVividValue = _firstVividPercent / 100f;
        SetVivid();
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
        _currentVividValue = Mathf.Min(MAX_VALUE, _currentVividValue + (_vividIncreasePercent / 100f));

        SetVivid();
    }

    private void DesreaseBrightnessValue()
    {
        _currentVividValue = Mathf.Max(MIN_VALUE, _currentVividValue - (_vividDecreasePercent / 100f));

        SetVivid();
    }

    private void SetVivid()
    {
        _vivid.SetFloat("_Saturation", _currentVividValue);
    }


#if UNITY_EDITOR
    //E10�����̃N���X�ł��BBrightness�̕ύX�́APlayMode�O�Ɉ����p����邽�߃��Z�b�g����{�^����p�ӂ��܂��B
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
                if (GUILayout.Button("Set Brightness"))
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
