using System;
using System.Collections;
using UnityEngine;

public interface IGhostManager
{
    bool IsKillPhase();
    void StartKillPhase();
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

    #region KillPhaseHandler
    public void StartKillPhase()
    {
        if (!_isGrace) TryRandom();
    }
    private void TryRandom()
    {
        float randomResults = Utils.Randomizer.GetFloat();
        if (randomResults > _thresholdKillPhase)
        {
            _isKillPhase = true;
            ChangeWorld();
            StartCoroutine(KillPhaseTimer());
        }
        else
        {
            StartCoroutine(RandomIntervalTimer());
        }
    }
    private void ChangeWorld()
    {
        ChangeWorldGM?.Invoke();
    }

    private IEnumerator GraceTimer()
    {
        yield return new WaitForSeconds(_graceTime);
        _isGrace = false;
    }

    private IEnumerator KillPhaseTimer()
    {
        yield return new WaitForSeconds(_killPhaseTime);
        _isKillPhase = false;
        _isGrace = true;
        ChangeWorld();
        StartCoroutine(GraceTimer());
    }

    private IEnumerator RandomIntervalTimer()
    {
        yield return new WaitForSeconds(_randomIntervalTime);
        TryRandom();
    }
    #endregion
}
