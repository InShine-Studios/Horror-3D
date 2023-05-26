using System;
using System.Collections;
using UnityEngine;

public interface IEnemyManager
{
    bool IsKillPhase();
}

public class EnemyManager : MonoBehaviour, IEnemyManager
{
    #region Events
    public static event Action ChangeWorldGM;
    #endregion

    #region Const
    private const float _checkRateChasing = 0.2f;
    #endregion

    #region Variable
    [SerializeField]
    private float _graceTime;
    [SerializeField]
    private float _killPhaseTime;
    [SerializeField]
    private float _randomIntervalTime;
    [SerializeField]
    [Range(0, 1)]
    private float _thresholdKillPhase;
    private EnemyFieldOfView _enemyFieldOfView;
    private EnemyStateMachine _enemyStateMachine;

    private bool _isKillPhase = false;
    #endregion

    #region SetGet
    public bool IsKillPhase()
    {
        return _isKillPhase;
    }
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        _enemyFieldOfView = GetComponent<EnemyFieldOfView>();
        _enemyStateMachine = GetComponent<EnemyStateMachine>();
        StartCoroutine(StartGracePeriod());
    }
    #endregion

    #region KillPhaseHandler
    private void AttemptKillPhase()
    {
        float randomResults = Utils.Randomizer.GetFloat();
        if (randomResults > _thresholdKillPhase)
        {
            _isKillPhase = true;
            ChangeWorld();
            StartCoroutine(CheckChasingRoutine());
            StartCoroutine(StartKillPhase());
        }
        else
        {
            StartCoroutine(StartAttemptKillPhaseCooldown());
        }
    }
    #endregion

    #region WorldChangeHandler
    private void ChangeWorld()
    {
        ChangeWorldGM?.Invoke();
    }
    #endregion

    #region IEnumeratorFunction
    private IEnumerator StartGracePeriod()
    {
        yield return new WaitForSeconds(_graceTime);
        AttemptKillPhase();
    }

    private IEnumerator StartKillPhase()
    {
        yield return new WaitForSeconds(_killPhaseTime);
        _isKillPhase = false;
        ChangeWorld();
        StopAllCoroutines();
        _enemyStateMachine.ChangeState<EnemyIdleState>();
        StartCoroutine(StartGracePeriod());
    }

    private IEnumerator StartAttemptKillPhaseCooldown()
    {
        yield return new WaitForSeconds(_randomIntervalTime);
        AttemptKillPhase();
    }

    private IEnumerator CheckChasingRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(_checkRateChasing);

        while (true)
        {
            yield return wait;
            bool playerSeen = _enemyFieldOfView.FieldOfViewCheck();
            if (playerSeen)
            {
                //Debug.Log("[ENEMY VISION] Player sighted.");
                _enemyStateMachine.AttemptChasing();
            }
        }
    }
    #endregion
}
