using System;
using UnityEngine;

public class Thrower : MonoBehaviour
{  
    private Camera _camera;
    private Vector3 _hitPoint;

    public event Action OnThrow;

    private void Start()
    {
        _camera = Camera.main;
    }

    public void CalculateTrajectory()
    {
        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
        {
            OnThrow?.Invoke();
            _hitPoint = hit.point;
        }
    }

    private void ThrowAmmoToPoint(Ammo ammo, Vector3 point)
    {
        ammo.transform.LookAt(point);
        ammo.MoveTo(point);   
    }

    public void ThrowAmmo(Ammo ammo)
    {
        ThrowAmmoToPoint(ammo, _hitPoint);
    }
}
