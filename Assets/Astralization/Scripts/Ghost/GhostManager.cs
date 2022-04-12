using System;
using System.Collections;
using UnityEngine;

public interface IGhostManager
{
    bool IsKillPhase();
    void FinishGracePeriod();
}

public class GhostManager : MonoBehaviour, IGhostManager
{
    #region Events
    public static event Action ChangeWorldGM;
    #endregion

    #region Variable
    [SerializeField]
    private float _graceTime;
    [SerializeField]
    private float _killPhaseTime;
    [SerializeField]
    private float _randomIntervalTime;
    [SerializeField]
    [Range(0,1)]
    private float _thresholdKillPhase;

    private bool _isKillPhase = false;
    private bool _isGrace = false;
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
        StartCoroutine(StartGracePeriod());
    }
    #endregion

    #region KillPhaseHandler
    public void FinishGracePeriod()
    {
        if (!_isGrace) AttemptKillPhase();
    }
    private void AttemptKillPhase()
    {
        float randomResults = Utils.Randomizer.GetFloat();
        if (randomResults > _thresholdKillPhase)
        {
            _isKillPhase = true;
            ChangeWorld();
            StartCoroutine(StartKillPhase());
        }
        else
        {
            StartCoroutine(StartAttemptKillPhaseCooldown());
        }
    }
    private void ChangeWorld()
    {
        ChangeWorldGM?.Invoke();
    }

    private IEnumerator StartGracePeriod()
    {
        yield return new WaitForSeconds(_graceTime);
        _isGrace = false;
        AttemptKillPhase();
    }

    private IEnumerator StartKillPhase()
    {
        yield return new WaitForSeconds(_killPhaseTime);
        _isKillPhase = false;
        _isGrace = true;
        ChangeWorld();
        StartCoroutine(StartGracePeriod());
    }

    private IEnumerator StartAttemptKillPhaseCooldown()
    {
        yield return new WaitForSeconds(_randomIntervalTime);
        AttemptKillPhase();
    }
    #endregion
}
