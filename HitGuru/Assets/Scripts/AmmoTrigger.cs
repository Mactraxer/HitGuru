using UnityEngine;
using System;

public class AmmoTrigger : MonoBehaviour
{
    public event Action OnDetectObstacle;
    public event Action OnDetectEnemy;

    private void OnTriggerEnter(Collider other)
    {
        OnDetectObstacle?.Invoke();    
    }
}
