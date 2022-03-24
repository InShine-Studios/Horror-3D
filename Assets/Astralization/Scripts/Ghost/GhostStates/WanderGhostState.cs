using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWanderGhostState
{
    void SetWanderTarget(string wanderTarget, bool isShifted);
}

public class WanderGhostState : GhostState, IWanderGhostState
{
    #region Variables
    private GhostMovement _ghostMovement;
    [Tooltip("Current delay in seconds for checking")]
    private float _checkRate = 1f;
    [Tooltip("Checker delay repeater")]
    private Utils.CooldownHelper _checker;
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
        StartWandering();
    }
    #endregion

    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        _checker = new Utils.CooldownHelper(_checkRate);
        _ghostMovement = GetComponent<GhostMovement>();
        _wanderTarget = "";
    }

    protected void Update()
    {
        _checker.AddAccumulatedTime();
        if (_checker.IsFinished())
        {
            _checker.SetAccumulatedTime(0f);
            if (!_ghostMovement.IsOnRoute())
            {
                owner.ChangeState<IdleGhostState>();
            }
        }
    }
    #endregion

    #region Handler
    public override void Enter()
    {
        base.Enter();
        _wanderTarget = "";
        _isShifted = false;
        StartWandering();
    }

    public override void Exit()
    {
        base.Exit();
        _ghostMovement.ResetPath();
    }

    private void StartWandering()
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
