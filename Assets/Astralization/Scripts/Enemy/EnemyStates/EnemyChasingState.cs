using System.Collections;
using UnityEngine;

public class EnemyChasingState : EnemyState
{
    #region Const
    private const float _checkRateChasing = 0.2f;
    #endregion

    #region Variables
    private EnemyMovement _enemyMovement;
    private EnemyFieldOfView _enemyFieldOfView;

    private EnemyTransitionZone _currentTransitionZone;
    #endregion

    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        _enemyMovement = GetComponent<EnemyMovement>();
        _enemyFieldOfView = GetComponent<EnemyFieldOfView>();
        debugMaterial = Resources.Load("EvidenceItem/MAT_Thermometer_Negative", typeof(Material)) as Material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyTransitionZone"))
        {
            _currentTransitionZone = other.GetComponent<EnemyTransitionZone>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("EnemyTransitionZone"))
        {
            _currentTransitionZone = null;
        }
    }
    #endregion

    #region StateHandler
    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        _enemyMovement.ResetPath();
        _enemyFieldOfView.ChasingTarget = null;
    }
    #endregion

    #region ChasingHandler
    public void EnemyChasing()
    {
        if (!(_enemyFieldOfView.ChasingTarget is null))
        {
            //Debug.Log("[ENEMY VISION] Player sighted.");
            _enemyMovement.WanderTarget(_enemyFieldOfView.ChasingTarget.position);
        }
        else if (!(_currentTransitionZone is null))
        {
            //Debug.Log("[ENEMY VISION] Lost sight of player. Attempting to move out of transition zone.");
            _enemyFieldOfView.ResetTarget();
            _enemyMovement.WanderTarget(_currentTransitionZone.ExitPoint.position);
        }
        else
        {
            //Debug.Log("[ENEMY VISION] Lost sight of player.");
            _enemyFieldOfView.ResetTarget();
            owner.ChangeState<EnemyIdleState>();
        }
    }
    #endregion
}
