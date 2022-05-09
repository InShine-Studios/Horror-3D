using UnityEngine;
using UnityEngine.AI;

public interface IGhostMovement
{
    NavMeshAgent NavMeshAgent { get; }

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
    #region Const
    private const float _wanderRange = 3f;
    #endregion

    #region Variables
    [SerializeField]
    [Tooltip("Consider ghost is arrive at destination if distance between ghost position and destination is less than thresh")]
    private float _distanceThresh = 0.5f;
    public NavMeshAgent NavMeshAgent { get; private set; }
    private NavMeshHit _navMeshHit;

    [Tooltip("Ghost current room")]
    private string _currentRoom = "Living Room";
    [Tooltip("Target destination of movement")]
    private Vector3 _wanderTarget;

    [SerializeField]
    private StageManager _stageManager;
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
        NavMeshAgent = GetComponent<NavMeshAgent>();
        _wanderTarget = transform.position;
        NavMeshAgent.SetDestination(_wanderTarget);
    }
    #endregion

    #region WanderingHandler
    private Vector3 RandomShiftTarget(StagePoint target)
    {
        float shiftX = Utils.Randomizer.GetFloat(-target.Radius, target.Radius);
        float shiftZ = Utils.Randomizer.GetFloat(-target.Radius, target.Radius);
        return target.GetPosition() + new Vector3(shiftX, 0, shiftZ);
    }

    public void WanderTarget(string targetRoomName, bool randomizePoint)
    {
        StagePoint targetRoom = _stageManager.GetRoomCoordinate(targetRoomName);
        if (WanderTarget(targetRoom, out _wanderTarget, randomizePoint))
        {
            NavMeshAgent.SetDestination(_wanderTarget);
        }
    }

    public void WanderTarget(Vector3 targetPosition)
    {
        NavMeshAgent.ResetPath();
        NavMeshAgent.SetDestination(targetPosition);
    }

    private bool WanderTarget(StagePoint targetRoom, out Vector3 result, bool randomizePoint)
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
            result = _wanderTarget;
            return false;
        }
    }

    private bool RandomWanderTarget(out Vector3 result)
    {
        StagePoint targetRoom = _stageManager.GetRandomRoomCoordinate();
        return WanderTarget(targetRoom, out result, true);
    }

    public void RandomWander()
    {
        if (RandomWanderTarget(out _wanderTarget))
        {
            NavMeshAgent.SetDestination(_wanderTarget);
            //Debug.DrawRay(_wanderTarget, Vector3.up, Color.blue, 1.0f);
        }
    }

    public void ResetPath()
    {
        NavMeshAgent.isStopped = true;
        NavMeshAgent.ResetPath();
    }
    #endregion
}
