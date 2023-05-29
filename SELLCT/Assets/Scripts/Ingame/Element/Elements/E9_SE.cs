using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E9_SE : Card
{
    [Header("所持枚数によったプロパティの値\n例：所持枚数が4枚の場合 Listのエレメント番号「2」の値を1")]
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
