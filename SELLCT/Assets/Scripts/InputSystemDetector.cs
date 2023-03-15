using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystemDetector : MonoBehaviour
{
    public event Action<InputAction.CallbackContext> OnNavigateAction;

    //InputSystem���ŌĂяo���Ă��܂��B
    //���̃��\�b�h�ɎQ�Ƃ�����Αz�肵�Ă��Ȃ����������Ă�����̂Ǝv���܂��B
    public void OnNavigate(InputAction.CallbackContext context)
    {
        OnNavigateAction?.Invoke(context);
    }
}