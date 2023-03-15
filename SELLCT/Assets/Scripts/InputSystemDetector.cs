using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystemDetector : MonoBehaviour
{
    public event Action<InputAction.CallbackContext> OnNavigateAction;

    //InputSystem側で呼び出しています。
    //このメソッドに参照があれば想定していない挙動をしているものと思われます。
    public void OnNavigate(InputAction.CallbackContext context)
    {
        OnNavigateAction?.Invoke(context);
    }
}