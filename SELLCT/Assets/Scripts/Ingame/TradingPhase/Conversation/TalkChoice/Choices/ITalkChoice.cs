using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITalkChoice
{
    /// <summary>
    /// 選択肢ID
    /// </summary>
    int Id { get; }

    /// <summary>
    /// 表示されるテキスト
    /// </summary>
    string Text { get; }

    /// <summary>
    /// この選択肢が有効か無効かを返す
    /// </summary>
    bool Enabled { get; }

    /// <summary>
    /// 選択した場合の処理
    /// </summary>
    void Select();

    /// <summary>
    /// エレメントにより有効にされた際の処理
    /// </summary>
    void Enable();

    /// <summary>
    /// エレメントにより無効にされた際の処理
    /// </summary>
    void Disable();
}
