using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EEX_null : Card
{
    [SerializeField] CardParameter _cardParameter;

    private static EEX_null instance;

    public static EEX_null Instance
    {
        get
        {
            //インスタンス生成前に取得しようとしたら、強制的に発見させる。
            instance ??= FindObjectOfType<EEX_null>();

            return instance;
        }
    }

    private void Awake()
    {
        instance ??= this;
    }

    public override int Rarity => throw new System.NotImplementedException();

    public override bool IsDisposedOfAfterSell => throw new System.NotImplementedException();

    public override Sprite CardSprite => throw new System.NotImplementedException();

    public override void Buy()
    {
        throw new System.NotImplementedException();
    }
    public override void Passive()
    {
        throw new System.NotImplementedException();
    }
    public override void Sell()
    {
        throw new System.NotImplementedException();
    }
}
