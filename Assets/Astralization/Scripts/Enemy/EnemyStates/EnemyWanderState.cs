using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyWanderState
{
    void SetWanderTarget(string wanderTarget, bool isShifted);
}

public class EnemyWanderState : EnemyState, IEnemyWanderState
{
    #region Variables
    private EnemyMovement _enemyMovement;
    [Tooltip("Current delay in seconds for checking")]
    private float _checkRate = 1f;
    [Tooltip("Room name for wander target. Enemy will wander randomly if wander target is not specified.")]
    private string _wanderTarget;
    [Tooltip("True if wander target randomly shifted")]
    private bool _isShifted;
    #endregion

    #region SetGet
    public void SetWanderTarget(string wanderTarget, bool isShifted)
    {
        _wanderTarget = wanderTarget;
        _isShifted = isShifted;
        _enemyMovement.ResetPath();
        EnemyWander();
    }
    #endregion

    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        _enemyMovement = GetComponent<EnemyMovement>();
        _wanderTarget = "";
        _isShifted = true;
        debugMaterial = Resources.Load("EvidenceItem/MAT_Thermometer_Active", typeof(Material)) as Material;
    }
    #endregion

    #region EnemyStateHandler
    public override void Enter()
    {
        base.Enter();
        EnemyWander();
        StartCoroutine(CheckEnemyRoutine());
    }

    public override void Exit()
    {
        base.Exit();
        _enemyMovement.ResetPath();
        StopCoroutine(CheckEnemyRoutine());
    }
    #endregion

    #region WanderingHandler
    protected IEnumerator CheckEnemyRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(_checkRate);

        while (true)
        {
            yield return wait;
            CheckEnemyPosition();
        }
    }

    private void CheckEnemyPosition()
    {
        if (!_enemyMovement.IsOnRoute())
        {
            owner.ChangeState<EnemyIdleState>();
        }
    }

    private void EnemyWander()
    {
        if (_wanderTarget == "")
        {
            _enemyMovement.RandomWander();
        }
        else
        {
            _enemyMovement.WanderTarget(_wanderTarget, _isShifted);
        }
    }
    #endregion
}
