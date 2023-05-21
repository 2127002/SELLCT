using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystemManager : MonoBehaviour
{
    public static InputSystemManager Instance { get; private set; }

    [SerializeField] InputActionAsset action;

    private void Awake()
    {
        Instance = this;
    }

    public void ActionEnable()
    {
        action.Enable();
    }    
    
    public void ActionDisable()
    {
        action.Disable();
    }
}
