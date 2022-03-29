using UnityEngine;
using UnityEngine.AI;

public interface IGhostMovement
{
    float GetDistanceThreshold();
    bool IsOnRoute();
    void WanderTarget(string targetRoomName, bool randomizePoint);
}

/*
 * GhostMovement
 * Controls ghost movement's target and delay.
 * Ghost movement target is defined by sampling position from NavMesh.
 */
public class GhostMovement : MonoBehaviour, IGhostMovement
{
    #region Variables
    [SerializeField]
    [Tooltip("Consider ghost is arrive at destination if distance between ghost position and destination is less than thresh")]
    private float _distanceThresh = 0.5f;
    [SerializeField]
    [Tooltip("Wander range for sampling. Should be less than 2x agent height")]
    private const float _wanderRange = 3f;
    private NavMeshAgent _navMeshAgent;
    private NavMeshHit _navMeshHit;

    [Tooltip("Ghost current room")]
    private string _currentRoom = "Living Room";
    [Tooltip("Target destination of movement")]
    private Vector3 _wanderTarget;
    #endregion

    #region SetGet
    private float GetDistanceFromDestination()
    {
        return Mathf.Abs(Utils.GeometryCalcu.GetDistance3D(transform.position, _wanderTarget));
    }

    public float GetDistanceThreshold()
    {
        return _distanceThresh;
    }

    public bool IsOnRoute()
    {
        return GetDistanceFromDestination() > _distanceThresh;
    }
    #endregion

    #region MonoBehaviour
    void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _wanderTarget = transform.position;
        _navMeshAgent.SetDestination(_wanderTarget);
    }
    #endregion

    #region Wandering Controller
    private Vector3 RandomShiftTarget(WorldPoint target)
    {
        float shiftX = Utils.Randomizer.GetFloat(-target.Radius, target.Radius);
        float shiftZ = Utils.Randomizer.GetFloat(-target.Radius, target.Radius);
        return target.GetPosition() + new Vector3(shiftX, 0, shiftZ);
    }

    public void WanderTarget(string targetRoomName, bool randomizePoint)
    {
        WorldPoint targetRoom = StageManager.GetRoomCoordinate(targetRoomName);
        if (WanderTarget(_wanderTarget, out _wanderTarget, targetRoom, randomizePoint))
        {
            _navMeshAgent.SetDestination(_wanderTarget);
        }
    }

    private bool WanderTarget(Vector3 center, out Vector3 result, WorldPoint targetRoom, bool randomizePoint)
    {
        Vector3 targetPoint = targetRoom.GetPosition();
        if (randomizePoint)
        {
            targetPoint = RandomShiftTarget(targetRoom);
        }
        if (NavMesh.SamplePosition(targetPoint, out _navMeshHit, _wanderRange, NavMesh.AllAreas))
        {
            result = _navMeshHit.position;
            _currentRoom = targetRoom.PointName;
            //Debug.Log("[GHOST MOVEMENT] Sampling target position. Target Room: " + _currentRoom + " with coordinate " + result);
            return true;
        }
        else
        {
            result = center;
            return false;
        }
    }

    private bool RandomWanderTarget(Vector3 center, out Vector3 result)
    {
        WorldPoint targetRoom = StageManager.GetRandomRoomCoordinate();
        return WanderTarget(center, out result, targetRoom, true);
    }

    public void RandomWander()
    {
        if (RandomWanderTarget(transform.position, out _wanderTarget))
        {
            _navMeshAgent.SetDestination(_wanderTarget);
            //Debug.DrawRay(_wanderTarget, Vector3.up, Color.blue, 1.0f);
        }
    }

    public void ResetPath()
    {
        _navMeshAgent.isStopped = true;
        _navMeshAgent.ResetPath();
    }
    #endregion
}
