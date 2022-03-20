using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderGhostState : GhostState
{
    #region Variables
    private GhostMovement _ghostMovement;
    [SerializeField]
    [Tooltip("Current delay in seconds for checking")]
    private float _checkRate = 1f;
    [Tooltip("Checker delay repeater")]
    private Utils.CooldownHelper _checker;
    [SerializeField]
    [Tooltip("Room name for wander target. Ghost will wander randomly if wander target is not specified.")]
    private string _wanderTarget;
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
        if (_wanderTarget == "")
        {
            _ghostMovement.RandomWander();
        }
        else
        {
            _ghostMovement.WanderTarget(_wanderTarget, true);
        }
    }

    public override void Exit()
    {
        base.Exit();
        _ghostMovement.ResetPath();
    }
    #endregion
}
