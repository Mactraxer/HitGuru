using System;
using System.Collections;
using UnityEngine;

public class Thrower : MonoBehaviour
{  
    private Camera _camera;
    private Vector3 _hitPoint;
    [SerializeField] private float _flySpeed = 0.3f;
    [SerializeField] private float _rotateSpeed = 10f;

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

    public void ThrowAmmo(GameObject ammo)
    {
        ThrowAmmoToPoint(ammo, _hitPoint);
    }

    public void StopThrowing()
    {
        StopAllCoroutines();
    }

    private void ThrowAmmoToPoint(GameObject ammo, Vector3 point)
    {
        ammo.transform.LookAt(point);

        StartCoroutine(MoveAmmoCoroutine(ammo.transform, point));
        StartCoroutine(RotateAmmoCoroutine(ammo.transform, _rotateSpeed));
    }

    private IEnumerator RotateAmmoCoroutine(Transform ammoTransform, float rotateSpeed)
    {
        var waitForFixedUpdate = new WaitForFixedUpdate();
        while (true)
        {
            ammoTransform.Rotate(Vector3.right, rotateSpeed);
            yield return waitForFixedUpdate;
        }
    }

    private IEnumerator MoveAmmoCoroutine(Transform ammoTransform, Vector3 distanseVector)
    {
        var waitForFixedUpdate = new WaitForFixedUpdate();
        while (ammoTransform.position != distanseVector)
        {

            ammoTransform.position = Vector3.MoveTowards(ammoTransform.position, distanseVector, _flySpeed);
            yield return waitForFixedUpdate;
        }
    }
}
