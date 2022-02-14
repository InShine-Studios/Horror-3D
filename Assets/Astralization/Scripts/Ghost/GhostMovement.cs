using UnityEngine;
using UnityEngine.AI;

public interface IGhostMovement
{
    float GetDistanceThreshold();
    bool IsOnRoute();
    void SetWandering(bool value);
    void WanderTarget(WorldPoint targetRoom, bool randomizePoint);
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
    [Tooltip("Cooldown of checking if target position should be updated")]
    private Utils.Range _checkRateRange;
    [Tooltip("Current delay for checking")]
    private float _checkRate;
    [Tooltip("Next check time")]
    private float _nextCheck = 0.0f;
    [Tooltip("Wander range for sampling. Should be less than 2x agent height")]
    private float _wanderRange = 3f;
    [Tooltip("True if ghost is able to wander")]
    private bool _enableWandering = true;
    private NavMeshAgent _navMeshAgent;
    private NavMeshHit _navMeshHit;

    [Tooltip("Ghost current room")]
    private string _currentRoom = "Living Room";

    //Uncomment SerializeField for Debug Purpose
    //[SerializeField]
    [Tooltip("Target destination of movement")]
    private Vector3 _wanderTarget;

    [SerializeField]
    private StageManager _stageManager;
    #endregion

    #region Awake - Update
    void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _wanderTarget = transform.position;
        _navMeshAgent.SetDestination(_wanderTarget);
    }

    void Update()
    {
        if (Time.time > _nextCheck)
        {
            _nextCheck = Time.time + _checkRate;
            _checkRate = Utils.Randomizer.GetFloat(_checkRateRange.min, _checkRateRange.max);

            if (_enableWandering) CheckReadyToWander();
        }
    }
    #endregion

    #region Setter - Getter
    private float GetDistanceFromDestination()
    {
        return Mathf.Abs(Utils.GeometryCalcu.GetDistance3D(transform.position,_wanderTarget));
    }

    public float GetDistanceThreshold()
    {
        return _distanceThresh;
    }

    public bool IsOnRoute()
    {
        bool isArrived = GetDistanceFromDestination() <= _distanceThresh;
        return !isArrived;
    }

    public void SetWandering(bool value)
    {
        _enableWandering = value;
    }
    #endregion

    #region Wandering Controller
    private Vector3 RandomShiftTarget(WorldPoint target)
    {
        float shiftX = Utils.Randomizer.GetFloat(-target.Radius, target.Radius);
        float shiftZ = Utils.Randomizer.GetFloat(-target.Radius, target.Radius);
        return target.GetPosition() + new Vector3(shiftX, 0, shiftZ);
    }

    public void WanderTarget(WorldPoint targetRoom, bool randomizePoint)
    {
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
            //Debug.Log("[GHOST] Sampling target position. Target Room: " + _currentRoom + " with coordinate " + result);
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
        WorldPoint targetRoom = _stageManager.GetRandomRoomCoordinate();
        return WanderTarget(center, out result, targetRoom, true);
    }

    private void CheckReadyToWander()
    {
        if (!IsOnRoute())
        {
            if (RandomWanderTarget(transform.position, out _wanderTarget))
            {
                _navMeshAgent.SetDestination(_wanderTarget);
                //Debug.DrawRay(_wanderTarget, Vector3.up, Color.blue, 1.0f);
            }
        }
    }
    #endregion
}
