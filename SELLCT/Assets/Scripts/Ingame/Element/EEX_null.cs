using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EEX_null : Card
{
    private static EEX_null instance;

    public static EEX_null Instance
    {
        get
        {
            //インスタンス生成前に取得しようとしたら、強制的に発見させる。
            if (instance == null) instance = FindObjectOfType<EEX_null>();

            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null) instance = this;
    }

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
