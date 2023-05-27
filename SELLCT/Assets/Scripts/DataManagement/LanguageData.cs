/// 言語ファイル　暗号化するため基本規則はセーブデータと同じ

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LanguageData
{
    public string gameTitleBar = "団道";

    public string gameStart = "ゲームスタート";

    public string keyConfigMove = "移動";
    public string keyConfigLookRotation = "回転";
    public string keyConfigJump = "ジャンプ";
    public string keyConfigAttack = "突き刺し";
    public string keyConfigEat = "食べる";
    public string keyConfigRemove = "取り外し";
    public string keyConfigPause = "ポーズ";
    public string keyConfigUIExpansion = "UI拡張";
}
