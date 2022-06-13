using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AmmoTrigger))]
public abstract class Ammo: MonoBehaviour
{
    public event Action<Ammo> OnDetectObstacle;
    public event Action<Ammo> OnDetectEmeny;

    private AmmoTrigger _trigger;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _trigger = GetComponent<AmmoTrigger>();

        _trigger.OnDetectObstacle += DetectObstacleHander;
        _trigger.OnDetectEnemy += DetectEnemyHandler;
    }

    public void Stop()
    {
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
}
