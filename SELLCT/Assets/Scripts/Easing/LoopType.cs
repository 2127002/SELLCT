using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LoopType
{
    /// <summary>
    /// 移動後、最初の位置に戻るように動く
    /// </summary>
    Yoyo,
    /// <summary>
    /// 移動後、最初の位置から再び移動する
    /// </summary>
    Restart,
    /// <summary>
    /// 移動後、その位置から再び移動する
    /// </summary>
    Increment
}