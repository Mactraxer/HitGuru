using UnityEngine;
using UnityEngine.AI;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform[] _wayPoints;
    private int _currentWayPoint;

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
        _currentWayPoint++;
    }
}
