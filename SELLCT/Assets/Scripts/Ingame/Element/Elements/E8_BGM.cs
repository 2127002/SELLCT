using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E8_BGM : Card
{
    [Header("所持枚数によったプロパティの値\n例：所持枚数が4枚の場合 Listのエレメント番号「2」の値を1")]
    [SerializeField, Range(0f, 1f)] List<float> _BGMValue = default!;

    public override int Id => 8;

    public override void Buy()
    {
        base.Buy();

        SetBGMValue();
    }

    public override void OnPressedU6Button()
    {
        throw new System.NotImplementedException();
    }

    public override void Sell()
    {
        base.Sell();

        SetBGMValue();
    }

    private void SetBGMValue()
    {
        float volume = _BGMValue[FindAll];

        SoundManager.Instance.SetAudioMixerValue(MixerGroup.BGM, volume);
    }
}
