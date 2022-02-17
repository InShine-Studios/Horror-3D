using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAstralMeterLogic
{
    void ChangeSightState();
    float GetAstralMeter();
    float GetConstantRate();
    bool IsOnSight();
    void NpcWrongAnswer();
    void PlayerKilled();
}

public class AstralMeterLogic : MonoBehaviour, IAstralMeterLogic
{
    #region Variables
    [Header("Current Astral Meter")]
    [SerializeField]
    private float _astralMeter = 0.0f;

    [Header("Max Astral Meter")]
    [SerializeField]
    private float _maxMeter = 100.0f;

    [Header("Astral Meter Rate")]
    [SerializeField]
    private float _constantRate = 0.05f;

    [Header("Astral Meter increment amount when seen by Ghost")]
    [SerializeField]
    private float _sightAmount = 1.0f;

    private bool _isOnSight = false;
    #endregion

    void Start()
    {
        InvokeRepeating("Increment", 0.0f, 1.0f);
    }

    #region Enable - Disable
    private void OnEnable()
    {
        GameManager.ChangeWorldEvent += ChangeWorld;
    }

    private void OnDisable()
    {
        GameManager.ChangeWorldEvent -= ChangeWorld;
    }
    #endregion

    #region Getter
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
    #endregion

    void Increment()
    {
        float currentMeter = 0.0f;
        if (_isOnSight)
        {
            currentMeter += _sightAmount;
        }
        currentMeter += _constantRate;
        _astralMeter = System.Math.Min(_maxMeter, _astralMeter + currentMeter);
    }

    #region World State
    private void ChangeWorld(bool state)
    {
        if (state)
        {
            _constantRate = 0.083f;
        }
        else _constantRate = 0.05f;
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

    #region NPC
    public void NpcWrongAnswer()
    {
        int randAnswerAmount = Random.Range(10, 15);
        _astralMeter += (float)randAnswerAmount;
    }
    #endregion
}
