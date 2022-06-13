using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerInput : MonoBehaviour
{
    public event Action OnInputTap;
    private PlayerInputActions _inputActions;

    private void Start()
    {
        _inputActions = new PlayerInputActions();
        _inputActions.Enable();

        _inputActions.Player.Tap.performed += TapInputed;
    }
    private void Update()
    {
        //GetInput();
    }

    private void GetInput()
    {
        if (Input.GetMouseButtonUp(0))
        { 
            OnInputTap?.Invoke();
        }
    }

    private void TapInputed(InputAction.CallbackContext context)
    {
        OnInputTap?.Invoke();
    }
}
