using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAstralMeterLogic
{
    void ChangeSightState();
    float GetAstralMeter();
    float GetAstralRate();
    float GetConstantRate();
    float GetRealRate();
    bool IsOnSight();
    void PlayerKilled();
    void SetAstralRate();
    void SetRealRate();
    void VictimWrongAnswer();
}

public class AstralMeterLogic : MonoBehaviour, IAstralMeterLogic
{
    #region Const
    private const float _astralRate = 0.083f;
    private const float _realRate = 0.05f;
    #endregion

    #region Variables
    [Header("Astral Meter")]
    [SerializeField]
    [Tooltip("Current Astral Meter")]
    private float _astralMeter = 0.0f;
    [SerializeField]
    [Tooltip("Max Astral Meter")]
    private float _maxMeter = 100.0f;

    [Header("Astral Meter Incrementation")]
    [SerializeField]
    [Tooltip("Rate of Astral Meter")]
    private float _constantRate = 0.05f;
    [SerializeField]
    [Tooltip("Astral Meter increase amount when seen by ghost")]
    private float _sightAmount = 1.0f;
    private bool _isOnSight = false;
    private bool _isInAstralWorld = false;
    #endregion

    #region SetGet
    public float GetAstralMeter()
    {
        return _astralMeter;
    }

    public float GetConstantRate()
    {
        return _constantRate;
    }

    public bool IsOnSight()
    {
        return _isOnSight;
    }

    public void SetAstralRate()
    {
        _constantRate = _astralRate;
    }

    public void SetRealRate()
    {
        _constantRate = _realRate;
    }

    public float GetAstralRate()
    {
        return _astralRate;
    }

    public float GetRealRate()
    {
        return _realRate;
    }
    #endregion

    #region MonoBehaviour
    void Awake()
    {
        InvokeRepeating(nameof(Increment), 0.0f, 1.0f);
    }
    #endregion

    #region Incrementer
    private void Increment()
    {
        float currentMeter = 0.0f;
        if (_isOnSight)
        {
            currentMeter += _sightAmount;
        }
        currentMeter += _constantRate;
        _astralMeter = System.Math.Min(_maxMeter, _astralMeter + currentMeter);
    }
    #endregion

    #region Ghost
    public void ChangeSightState()
    {
        _isOnSight = !_isOnSight;
    }

    public void PlayerKilled()
    {
        int randKillAmount = Random.Range(15, 20);
        _astralMeter += (float)randKillAmount;
    }
    #endregion

    #region Victim
    public void VictimWrongAnswer()
    {
        int randAnswerAmount = Random.Range(10, 15);
        _astralMeter += (float)randAnswerAmount;
    }
    #endregion
}
