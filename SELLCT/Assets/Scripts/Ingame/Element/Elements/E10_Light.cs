using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.PackageManager.UI;
#endif
using UnityEngine;

public class E10_Light : Card
{
    [SerializeField] Material _brightness = default!;
    [Header("�������x�����ɂ�����v���p�e�B�̒l\n��F����������4���̏ꍇ List�̃G�������g�ԍ��u4�v�̒l��100(%)")]
    [SerializeField, Min(0)] List<float> _brightnessValue = default!;
    [SerializeField] PhaseController _phaseController = default!;

    [SerializeField] End_4 end_4;

    float _currentBrightnessValue = 1.0f;

    public override int Id => 10;

    private void Awake()
    {
        _phaseController.OnGameStart.Add(SetBrightnessValue);
    }

    private void OnDestroy()
    {
        _phaseController.OnGameStart.Remove(SetBrightnessValue);
    }

    public override void Buy()
    {
        base.Buy();
    }

    public override void OnPressedU6Button()
    {
        throw new System.NotImplementedException();
    }

    public override void Sell()
    {
        if (GameOverChecker()) return;
   
        base.Sell();
    }
    private bool GameOverChecker()
    {
        //�������ۂ�1�����Ȃ��Ȃ�Q�[���I�[�o�[
        if (_handMediator.ContainsCard(this)) return false;

        end_4.End_4Transition();
        return true;
    }
    public void SetBrightnessValue()
    {
        _currentBrightnessValue = _brightnessValue[FindAll] / 100f;

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
            if (_light == null)
            {
                EditorGUILayout.HelpBox("E10_Light object not found in scene.", MessageType.Warning);
                if (GUILayout.Button("Reload"))
                {
                    BrightnessResetWindow window = GetWindow<BrightnessResetWindow>("Brightness Changer");
                    E10_Light light = FindObjectOfType<E10_Light>();
                    window.SetLight(light);
                }
                return;
            }

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
    }
#endif
}