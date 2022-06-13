using UnityEngine;
using System;

[RequireComponent(typeof(Collider))]
public class EnemyTrigger : MonoBehaviour 
{
    public event Action<Ammo> OnTakeHit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Ammo ammo))
        {
            OnTakeHit?.Invoke(ammo);
        }
    }
}
