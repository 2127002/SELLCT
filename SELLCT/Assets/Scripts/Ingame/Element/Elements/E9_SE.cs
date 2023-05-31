using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E9_SE : Card
{
    [Header("���������ɂ�����v���p�e�B�̒l\n��F����������4���̏ꍇ List�̃G�������g�ԍ��u2�v�̒l��1")]
    [SerializeField, Range(0f, 1f)] List<float> _SEValue = default!;

    public override int Id => 9;

    public override void Buy()
    {
        base.Buy();

        SetSEValue();
    }

    public override void OnPressedU6Button()
    {
        throw new System.NotImplementedException();
    }

    public override void Sell()
    {
        base.Sell();

        SetSEValue();
    }

    private void SetSEValue()
    {
        float volume = _SEValue[FindAll];

        SoundManager.Instance.SetAudioMixerValue(MixerGroup.SE, volume);
    }
}
