using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AmmoTrigger))]
public class Ammo : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField] Vector3 _pointForce;
    [SerializeField] AmmoTrigger _trigger;

    public event Action<Ammo> OnDetectObstacle;
    public event Action<Ammo> OnDetectEmeny;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _trigger = GetComponent<AmmoTrigger>();

        _trigger.OnDetectObstacle += DetectObstacleHander;
        _trigger.OnDetectEnemy += DetectEnemyHandler;
    }

    public void MoveTo(Vector3 newPosition)
    {
        StartCoroutine(MoveAmmoCoroutine(transform, newPosition));
        StartRotate();
    }

    private void StartRotate()
    {
        _rigidbody.AddTorque(Vector3.right, ForceMode.Force);
    }

    public void Stop()
    {
        StopAllCoroutines();
        _rigidbody.isKinematic = true;
    }

    public void SetRotate(Quaternion quaternion)
    {
        transform.SetPositionAndRotation(transform.position, quaternion);
    }

    private void DetectObstacleHander()
    {
        OnDetectObstacle?.Invoke(this);
    }

    private void DetectEnemyHandler()
    {
        OnDetectEmeny?.Invoke(this);
    }

    private IEnumerator MoveAmmoCoroutine(Transform ammoTransform, Vector3 distanseVector)
    {
        var waitForFixedUpdate = new WaitForFixedUpdate();
        while (ammoTransform.position != distanseVector)
        {

            ammoTransform.position = Vector3.MoveTowards(ammoTransform.position, distanseVector, 0.2f);
            yield return waitForFixedUpdate;
        }
    }
}
