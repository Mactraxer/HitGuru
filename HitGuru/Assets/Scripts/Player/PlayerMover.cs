using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform[] _wayPoints;
    private int _currentWayPoint;

    public event Action OnStartMove;
    public event Action OnStopMove;

    private void Start()
    {
        _currentWayPoint = 0;
    }

    public void MoveNextPoint()
    {
        if (_currentWayPoint >= _wayPoints.Length) return;

        _agent.SetDestination(_wayPoints[_currentWayPoint].position);
        OnStartMove?.Invoke();
        _currentWayPoint++;
        StartCoroutine(ReachDistinationCoroutine());
    }

    private IEnumerator ReachDistinationCoroutine()
    {
        var waitForFixedUpdate = new WaitForFixedUpdate();
        var distanceToPoint = (_agent.destination - transform.position).magnitude;
        
        while (distanceToPoint >= _agent.stoppingDistance)
        {
            yield return waitForFixedUpdate;
            distanceToPoint = (_agent.destination - transform.position).magnitude;
        }

        OnStopMove?.Invoke();
    }
}
