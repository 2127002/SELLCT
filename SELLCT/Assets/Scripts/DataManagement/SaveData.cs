/// セーブデータの実体クラス
/// float型やUnity独自の型は使えず
/// int、double、string、boolだけ使えると覚えておいてください
/// 特にfloatが使えないのは忘れがちなので注意
/// ネストしたオブジェクト型が使えるかは未検証
///
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public bool[] hasCollectedEndings = new bool[6] { false, true, false, false, false, true };
    //public bool[] hasCollectedEndings = new bool[6] { true, true, true, true, true, true };
    public int sceneNum = 0;

    //下記は書き方の例です。
    //public int a = 0;
    //public string b = "string";
    //public double c = 3.14d;
    //public bool d = false;
}
