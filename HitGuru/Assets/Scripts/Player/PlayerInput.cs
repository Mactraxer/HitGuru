using UnityEngine;
using System;

public class PlayerInput : MonoBehaviour
{
    public event Action OnInputTap;

    private void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnInputTap?.Invoke();
        }
    }
}
