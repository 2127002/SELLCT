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
    [Header("Defaultの明るさを100%として、以下の値で上昇します。\n例：値が10の場合 100%→110%")]
    [SerializeField, Range(0, 150f)] float _brightnessIncreasePercent;
    [Header("Defaultの明るさを100%として、以下の値で減少します。\n例：値が4.5の場合 55%→50.5%")]
    [SerializeField, Range(0, 150f)] float _brightnessDecreasePercent;

    [Header("初期の明度を設定します。Defaultの明るさは100%です。\nまた、初期のエレメント所持数に関係なくこの値になります。")]
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
            //初期化
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
        //最初の所持枚数と関係なく設定される。フェーズと関係なく設定するため、問題が生じたら変更推奨
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
    //E10内部のクラスです。Brightnessの変更は、PlayMode外に引き継がれるためリセットするボタンを用意します。
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