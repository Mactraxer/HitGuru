using UnityEngine;
using System;

public class AmmoTrigger : MonoBehaviour
{
    public event Action OnDetectObstacle;
    public event Action OnDetectEnemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            OnDetectObstacle?.Invoke();
        }
        else if (other.gameObject.TryGetComponent(out EnemyTrigger enemy))
        {
            OnDetectEnemy?.Invoke();
        }
    }
}
