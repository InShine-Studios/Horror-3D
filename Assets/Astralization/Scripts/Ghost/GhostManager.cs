using System;
using UnityEngine;

public interface IGhostManager
{
    bool IsKillPhase();
}

public class GhostManager : MonoBehaviour, IGhostManager
{
    #region Events
    public static event Action ChangeWorldGM;
    #endregion

    #region Variable
    private Utils.CooldownHelper _randomIntervalTimer;
    private Utils.CooldownHelper _killPhaseTimer;
    private Utils.CooldownHelper _graceTimer;

    [SerializeField]
    private float _graceTime;
    [SerializeField]
    private float _killPhaseTime;
    [SerializeField]
    private float _randomIntervalTime;

    private bool _isKillPhase = false;
    private bool _isGrace = true;
    private bool _isRandomInterval = false;
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        _randomIntervalTimer = new Utils.CooldownHelper(_randomIntervalTime);
        _killPhaseTimer = new Utils.CooldownHelper(_killPhaseTime);
        _graceTimer = new Utils.CooldownHelper(_graceTime);
    }

    private void Update()
    {
        if (_isGrace)
        {
            _graceTimer.AddAccumulatedTime();
            if (_graceTimer.IsFinished())
            {
                _isGrace = false;
                TryRandom();
                ResetTimer(_graceTimer);
            }
        }
        else if (_isKillPhase)
        {
            _killPhaseTimer.AddAccumulatedTime();
            if (_killPhaseTimer.IsFinished())
            {
                _isKillPhase = false;
                _isGrace = true;
                ChangeWorld();
                ResetTimer(_killPhaseTimer);
            }
        }
        else if (_isRandomInterval)
        {
            _randomIntervalTimer.AddAccumulatedTime();
            if (_randomIntervalTimer.IsFinished())
            {
                TryRandom();
                ResetTimer(_randomIntervalTimer);
            }
        }
    }
    #endregion

    #region SetGet
    public bool IsKillPhase()
    {
        return _isKillPhase;
    }
    #endregion

    #region Handler
    public void TryRandom()
    {
        float randomResults = Utils.Randomizer.GetFloat();
        if (randomResults > 0.5f)
        {
            _isKillPhase = true;
            _isRandomInterval = false;
            ChangeWorld();
        }
        else
        {
            _isRandomInterval = true;
        }
    }
    public void ChangeWorld()
    {
        ChangeWorldGM?.Invoke();
    }

    public void ResetTimer(Utils.CooldownHelper timer)
    {
        timer.SetAccumulatedTime(0f);
    }
    #endregion
}
