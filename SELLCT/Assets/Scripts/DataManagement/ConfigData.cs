/// 設定ファイルの実体クラス
/// 基本的にはセーブデータと扱いは同じ
/// Enum型は使えないのでシリアライズ化せずに後から変換してあります
using System;
using UnityEngine;

public enum Language
{
    Unknown,
    JP,
    US
}

[Serializable]
public class ConfigData
{
    //初期化状態 ※特殊なフラグなので設定ファイルに含めない
    //データが存在せず、ファイルが作成(リセット)された「一回目」のみ真になります
    [NonSerialized]
    public bool IsReset = false;

    //言語設定　※デシリアライズ出来ないので設定ファイルに含めない
    [NonSerialized]
    public Language language = Language.JP;

    //言語設定のデシリアライズ用
    public string languageString = "JP";

    //セーブデータのファイルパス
    public string dataFilePath = "default";

    //マスターボリューム
    public int masterVolume = 10;

    //サウンドエフェクトボリューム
    public int soundEffectVolume = 5;

    //バックグラウンドミュージックボリューム
    public int backGroundMusicVolume = 5;
}