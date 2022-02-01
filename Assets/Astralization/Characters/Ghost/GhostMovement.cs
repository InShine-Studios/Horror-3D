using UnityEngine;
using UnityEngine.AI;

public class GhostMovement : MonoBehaviour
{
    private bool _isOnRoute = false;
    private float _checkRate;
    private float _nextCheck = 0.0f;
    private float _wanderRange = 10;
    private NavMeshAgent _navMeshAgent;
    private NavMeshHit _navMeshHit;
    private Vector3 _wanderTarget;

    void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _checkRate = Utils.Randomizer.GetFloat(2f, 2.1f);
    }

    void Update()
    {
        if (Time.time > _nextCheck)
        {
            _nextCheck = Time.time + _checkRate;
            CheckReadyToWander();
        }
    }

    private void CheckReadyToWander()
    {
        if (!_isOnRoute)
        {
            if (RandomWanderTarget(transform.position, _wanderRange, out _wanderTarget))
            {
                _navMeshAgent.SetDestination(_wanderTarget);
                Debug.DrawRay(_wanderTarget, Vector3.up, Color.blue, 1.0f);
            }
        }
    }

    private bool RandomWanderTarget(Vector3 center, float range, out Vector3 result)
    {
        if (NavMesh.SamplePosition(center, out _navMeshHit, range, NavMesh.AllAreas))
        {
            result = _navMeshHit.position;
            return true;
        }
        else
        {
            result = center;
            return false;
        }
    }
}
