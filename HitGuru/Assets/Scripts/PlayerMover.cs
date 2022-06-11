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

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            MoveNextPoint();
        }
    }

    private void MoveNextPoint()
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
        
        while (_agent.destination != transform.position)
        {
            yield return waitForFixedUpdate;
        }

        OnStopMove?.Invoke();
    }
}
