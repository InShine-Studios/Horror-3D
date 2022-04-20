using System.Collections;
using UnityEngine;

public class GhostChasingState : GhostState
{
    #region Const
    private const float _checkRateChasing = 0.2f;
    #endregion

    #region Variables
    private GhostMovement _ghostMovement;
    private GhostFieldOfView _ghostFieldOfView;
    [Tooltip("Room name for wander target. Ghost will wander randomly if wander target is not specified.")]
    protected string _wanderTarget;
    [Tooltip("True if wander target randomly shifted")]
    protected bool _isShifted;

    private GhostTransitionZone _currentTransitionZone;
    #endregion

    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        _ghostMovement = GetComponent<GhostMovement>();
        _ghostFieldOfView = GetComponent<GhostFieldOfView>();
        debugMaterial = Resources.Load("EvidenceItem/MAT_Thermometer_Negative", typeof(Material)) as Material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GhostTransitionZone"))
        {
            _currentTransitionZone = other.GetComponent<GhostTransitionZone>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GhostTransitionZone"))
        {
            _currentTransitionZone = null;
        }
    }
    #endregion

    #region StateHandler
    public override void Enter()
    {
        base.Enter();
        _wanderTarget = "";
        _isShifted = true;
    }

    public override void Exit()
    {
        base.Exit();
        _ghostMovement.ResetPath();
        _ghostFieldOfView.ChasingTarget = null;
    }
    #endregion

    #region ChasingHandler
    public void GhostChasing()
    {
        if (!(_ghostFieldOfView.ChasingTarget is null))
        {
            //Debug.Log("[GHOST VISION] Player sighted.");
            _ghostMovement.WanderTarget(_ghostFieldOfView.ChasingTarget.position);
        }
        else if (!(_currentTransitionZone is null))
        {
            //Debug.Log("[GHOST VISION] Lost sight of player. Attempting to move out of transition zone.");
            _ghostFieldOfView.ResetTarget();
            _ghostMovement.WanderTarget(_currentTransitionZone.ExitPoint.position);
        }
        else
        {
            //Debug.Log("[GHOST VISION] Lost sight of player.");
            _ghostFieldOfView.ResetTarget();
            owner.ChangeState<GhostIdleState>();
        }
    }
    #endregion
}
