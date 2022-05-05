using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGhostWanderState
{
    void SetWanderTarget(string wanderTarget, bool isShifted);
}

public class GhostWanderState : GhostState, IGhostWanderState
{
    #region Variables
    private GhostMovement _ghostMovement;
    [Tooltip("Current delay in seconds for checking")]
    private float _checkRate = 1f;
    [Tooltip("Room name for wander target. Ghost will wander randomly if wander target is not specified.")]
    private string _wanderTarget;
    [Tooltip("True if wander target randomly shifted")]
    private bool _isShifted;
    #endregion

    #region SetGet
    public void SetWanderTarget(string wanderTarget, bool isShifted)
    {
        _wanderTarget = wanderTarget;
        _isShifted = isShifted;
        _ghostMovement.ResetPath();
        GhostWander();
    }
    #endregion

    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        _ghostMovement = GetComponent<GhostMovement>();
        _wanderTarget = "";
        _isShifted = true;
        debugMaterial = Resources.Load("EvidenceItem/MAT_Thermometer_Active", typeof(Material)) as Material;
    }
    #endregion

    #region GhostStateHandler
    public override void Enter()
    {
        base.Enter();
        GhostWander();
        StartCoroutine(CheckGhostRoutine());
    }

    public override void Exit()
    {
        base.Exit();
        _ghostMovement.ResetPath();
        StopCoroutine(CheckGhostRoutine());
    }
    #endregion

    #region WanderingHandler
    protected IEnumerator CheckGhostRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(_checkRate);

        while (true)
        {
            yield return wait;
            CheckGhostPosition();
        }
    }

    private void CheckGhostPosition()
    {
        if (!_ghostMovement.IsOnRoute())
        {
            owner.ChangeState<GhostIdleState>();
        }
    }

    private void GhostWander()
    {
        if (_wanderTarget == "")
        {
            _ghostMovement.RandomWander();
        }
        else
        {
            _ghostMovement.WanderTarget(_wanderTarget, _isShifted);
        }
    }
    #endregion
}
