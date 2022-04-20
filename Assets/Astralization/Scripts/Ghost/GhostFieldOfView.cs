using System.Collections;
using UnityEngine;

public interface IGhostFieldOfView
{
    Transform ChasingTarget { get; set; }

    bool FieldOfViewCheck();
    void ResetTarget();
}

public class GhostFieldOfView : MonoBehaviour, IGhostFieldOfView
{
    #region Const
    private const string _playerLayerName = "Player";
    private const string _buildingLayerName = "Building";
    private const string _hidingLayerName = "HidingPlace";
    private const float _checkRate = 0.2f;
    #endregion

    #region Variables
    [SerializeField]
    [Tooltip("Sight radius of the ghost")]
    private float radius = 10f;
    [SerializeField]
    [Tooltip("Sight angle of the ghost")]
    [Range(0, 360)]
    private float angle = 180f;

    [Tooltip("Target layer mask of ghost vision. By default: Player")]
    private LayerMask targetMask;
    [Tooltip("Target layer mask of ghost vision. By default: Building and HidingPlace")]
    private LayerMask obstructionMask;

    private GhostStateMachine _ghostStateMachine;
    public Transform ChasingTarget { get; set; }
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        _ghostStateMachine = GetComponent<GhostStateMachine>();

        targetMask = LayerMask.GetMask(_playerLayerName);
        obstructionMask = LayerMask.GetMask(_buildingLayerName, _hidingLayerName);
    }
    #endregion

    #region VisionHandler
    public bool FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        bool canSeePlayer = false;
        if (rangeChecks.Length != 0)
        {
            Transform chasingTarget = rangeChecks[0].transform;

            Vector3 directionToTarget = (chasingTarget.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, chasingTarget.position);

                canSeePlayer = !Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask);

                if (canSeePlayer)
                {
                    ChasingTarget = chasingTarget;
                }
            }
        }
        else
        {
            ChasingTarget = null;
        }
        return canSeePlayer;
    }

    public void ResetTarget()
    {
        ChasingTarget = null;
    }
    #endregion
}
