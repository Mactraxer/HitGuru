using UnityEngine;
using System.Collections;
using System;

//[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AmmoTrigger))]
public class Ammo : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField] private AmmoTrigger _trigger;
    [SerializeField] private Vector3 _positionSettings;
    [SerializeField] private Vector3 _rotateSettings;

    public event Action<Ammo> OnDetectObstacle;
    public event Action<Ammo> OnDetectEmeny;

    public Vector3 positionForSpawn => _positionSettings;
    public Vector3 rotationForSpawn => _rotateSettings;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _trigger = GetComponent<AmmoTrigger>();

        _trigger.OnDetectObstacle += DetectObstacleHander;
        _trigger.OnDetectEnemy += DetectEnemyHandler;
    }

    private void OnDestroy()
    {
        _trigger.OnDetectObstacle -= DetectObstacleHander;
        _trigger.OnDetectEnemy -= DetectEnemyHandler;
    }

    public void MoveTo(Vector3 newPosition)
    {
        StartCoroutine(MoveAmmoCoroutine(transform, newPosition));
        StartCoroutine(RotateAmmoCoroutine(transform, new Vector3(0.02f, 0, 0)));
    }

    public void Stop()
    {
        StopAllCoroutines();
        GetComponent<BoxCollider>().enabled = false;
        _rigidbody.isKinematic = true;
    }

    private void DetectObstacleHander()
    {
        OnDetectObstacle?.Invoke(this);
    }

    private void DetectEnemyHandler()
    {
        OnDetectEmeny?.Invoke(this);
    }

    private IEnumerator RotateAmmoCoroutine(Transform ammoTransform, Vector3 eulerVector)
    {
        var waitForFixedUpdate = new WaitForFixedUpdate();
        while (true)
        {
            ammoTransform.Rotate(Vector3.right, 10);
            yield return waitForFixedUpdate;
        }
    }

    private IEnumerator MoveAmmoCoroutine(Transform ammoTransform, Vector3 distanseVector)
    {
        var waitForFixedUpdate = new WaitForFixedUpdate();
        while (ammoTransform.position != distanseVector)
        {

            ammoTransform.position = Vector3.MoveTowards(ammoTransform.position, distanseVector, 0.3f);
            yield return waitForFixedUpdate;
        }
    }
}
