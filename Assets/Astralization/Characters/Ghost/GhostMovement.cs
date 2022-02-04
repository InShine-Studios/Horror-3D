using UnityEngine;
using UnityEngine.AI;

public class GhostMovement : MonoBehaviour
{
    private const float _thresh = 0.3f;
    private float _checkRate;
    private float _nextCheck = 0.0f;
    private float _wanderRange = 30f;
    private NavMeshAgent _navMeshAgent;
    private NavMeshHit _navMeshHit;
    private Vector3 _wanderTarget;

    public Vector3 WanderTarget;

    void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _checkRate = Utils.Randomizer.GetFloat(2f, 2.1f);
        _wanderTarget = transform.position;
        //DEBUG
        //_navMeshAgent.SetDestination(WanderTarget);
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
        if (!IsOnRoute())
        {
            if (RandomWanderTarget(WanderTarget, _wanderRange, out _wanderTarget))
            {
                Debug.Log("Wander Target: " + _wanderTarget);
                _navMeshAgent.SetDestination(_wanderTarget);
                Debug.DrawRay(_wanderTarget, Vector3.up, Color.blue, 1.0f);
            }
        }
    }

    private bool IsOnRoute()
    {
        return Mathf.Abs(Utils.GeometryCalcu.GetDistance3D(transform.position, _wanderTarget)) > _thresh;
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
