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

    private bool _enableChasing = true;
    private GhostTransitionZone _currentTransitionZone;
    #endregion

    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        _ghostMovement = GetComponent<GhostMovement>();
        _ghostFieldOfView = GetComponent<GhostFieldOfView>();
        debugMaterial = Resources.Load("EvidenceItem/MAT_Thermometer_Negative", typeof(Material)) as Material;
        if (_enableChasing)
        {
            StartCoroutine(CheckChasingRoutine()); // Will be moved to ghost manager that can trigger kill phase
        }
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
    }
    #endregion

    #region ChasingHandler
    private IEnumerator CheckChasingRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(_checkRateChasing);

        while (true)
        {
            yield return wait;
            bool playerSeen = _ghostFieldOfView.FieldOfViewCheck();
            if (playerSeen)
            {
                //Debug.Log("[GHOST VISION] Player sighted.");
                GhostChasing(_ghostFieldOfView.ChasingTarget);
            }
            else if (!(_currentTransitionZone is null))
            {
                //Debug.Log("[GHOST VISION] Lost sight of player. Attempting to move out of transition zone.");
                _ghostMovement.WanderTarget(_currentTransitionZone.ExitPoint.position);
            }
            else
            {
                //Debug.Log("[GHOST VISION] Lost sight of player.");
                //TODO change state to idle
            }
        }
    }

    protected void GhostChasing(Transform target)
    {
        _ghostMovement.WanderTarget(target.position);
    }
    #endregion
}
