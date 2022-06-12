using UnityEngine;
using System;

public class PlayerInput : MonoBehaviour
{
    public event Action OnInputMove;
    public event Action OnInputHit;

    private void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnInputHit?.Invoke();
        }

        if (Input.GetMouseButtonDown(1))
        {
            OnInputMove?.Invoke();
        }
    }
}
