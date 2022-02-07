using UnityEngine;
using UnityEngine.AI;


/*
 * GhostMovement
 * Controls ghost movement's target and delay.
 * Ghost movement target is defined by sampling position from NavMesh.
 */
public class GhostMovement : MonoBehaviour
{
    #region Variables
    [SerializeField]
    [Tooltip("Consider ghost is not moving if delta position is less than thresh")]
    private float _positionThresh = 0.01f;
    [SerializeField]
    [Tooltip("Cooldown of checking if target position should be updated")]
    private Utils.Range _checkRateRange;
    [Tooltip("Current delay for checking")]
    private float _checkRate;
    [Tooltip("Next check time")]
    private float _nextCheck = 0.0f;
    [Tooltip("Wander range for sampling. Should be less than 2x agent height")]
    private float _wanderRange = 3f;
    [Tooltip("Last position of ghost, updated on every frame")]
    private Vector3 _lastPosition;
    private NavMeshAgent _navMeshAgent;
    private NavMeshHit _navMeshHit;

    [Tooltip("Ghost current room")]
    private string _currentRoom = "Living Room";

    //Uncomment SerializeField for Debug Purpose
    //[SerializeField]
    [Tooltip("Target destination of movement")]
    private Vector3 _wanderTarget;
    #endregion

    #region Awake - Update
    void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _wanderTarget = transform.position;
        _lastPosition = transform.position;
        _navMeshAgent.SetDestination(_wanderTarget);
    }

    void Update()
    {
        if (Time.time > _nextCheck)
        {
            _nextCheck = Time.time + _checkRate;
            _checkRate = Utils.Randomizer.GetFloat(_checkRateRange.min, _checkRateRange.max);
            CheckReadyToWander();
        }
        _lastPosition = transform.position;
    }
    #endregion

    #region Setter - Getter
    private float GetDeltaPosition()
    {
        return Mathf.Abs(Utils.GeometryCalcu.GetDistance3D(_lastPosition, transform.position));
    }

    private bool IsOnRoute()
    {
        return GetDeltaPosition() > _positionThresh;
    }
    #endregion

    #region Wandering Controller
    private Vector3 RandomShiftTarget(RoomCoordinate target)
    {
        float shiftX = Utils.Randomizer.GetFloat(-target.radius, target.radius);
        float shiftZ = Utils.Randomizer.GetFloat(-target.radius, target.radius);
        return target.coordinate + new Vector3(shiftX, 0, shiftZ);
    }

    public bool WanderTarget(Vector3 center, out Vector3 result, RoomCoordinate targetRoom)
    {
        Vector3 targetPoint = RandomShiftTarget(targetRoom);
        if (NavMesh.SamplePosition(targetPoint, out _navMeshHit, _wanderRange, NavMesh.AllAreas))
        {
            result = _navMeshHit.position;
            _currentRoom = targetRoom.name;
            //Debug.Log("[GHOST] Sampling target position. Target Room: " + _currentRoom);
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
        RoomCoordinate targetRoom = StageController.GetRandomRoomCoordinate();
        return WanderTarget(center,out result, targetRoom);
    }

    private void CheckReadyToWander()
    {
        if (!IsOnRoute())
        {
            if (RandomWanderTarget(_wanderTarget, out _wanderTarget))
            {
                _navMeshAgent.SetDestination(_wanderTarget);
                Debug.DrawRay(_wanderTarget, Vector3.up, Color.blue, 1.0f);
            }
        }
    }
    #endregion
}
