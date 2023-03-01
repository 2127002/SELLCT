using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystemDetector : MonoBehaviour
{
    /// <summary>
    /// Awakeでインスタンスを取得しています。Awake以降で使用してください。
    /// </summary>
    public static InputSystemDetector Instance { get; private set; }

    Action<InputAction.CallbackContext> onNavigate;

    private void Awake()
    {
        Instance = this;
    }

    public void AddNavigate(Action<InputAction.CallbackContext> action)
    {
        onNavigate += action;
    }

    public void RemoveNavigate(Action<InputAction.CallbackContext> action)
    {
        onNavigate -= action;
    }

    public void OnNavigate(InputAction.CallbackContext context)
    {
        onNavigate?.Invoke(context);
    }
}
