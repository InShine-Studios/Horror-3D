using System.Collections;
using UnityEngine;

public interface IEnemyFieldOfView
{
    Transform ChasingTarget { get; set; }

    bool FieldOfViewCheck();
    void ResetTarget();
}

public class EnemyFieldOfView : MonoBehaviour, IEnemyFieldOfView
{
    #region Variables
    [SerializeField]
    [Tooltip("Sight radius of the enemy")]
    private float radius = 10f;
    [SerializeField]
    [Tooltip("Sight angle of the enemy")]
    [Range(0, 360)]
    private float angle = 180f;

    [Tooltip("Target layer mask of enemy vision. By default: Player")]
    private LayerMask targetMask;
    [Tooltip("Target layer mask of enemy vision. By default: Building and HidingPlace")]
    private LayerMask obstructionMask;

    public Transform ChasingTarget { get; set; }
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        targetMask = LayerMask.GetMask(CameraHelper.PlayerLayer);
        obstructionMask = LayerMask.GetMask(CameraHelper.BuildingLayer, CameraHelper.HidingPlaceLayer);
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
            }
        }

        if (canSeePlayer)
        {
            ChasingTarget = rangeChecks[0].transform;
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
