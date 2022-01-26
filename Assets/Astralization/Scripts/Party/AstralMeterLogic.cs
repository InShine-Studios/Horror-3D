using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAstralMeterLogic
{
    void ChangeSightState();
    void ChangeWorld();
    float GetAstralMeter();
    float GetConstantRate();
    bool IsOnRealWorld();
    bool IsOnSight();
    void NPCWrongAnswer();
    void PlayerKilled();
}

public class AstralMeterLogic : MonoBehaviour, IAstralMeterLogic
{
    private float _astralMeter = 0.0f;
    private float _maxMeter = 100.0f;
    private float _constantRate = 0.05f;
    private float _sightAmount = 1.0f;
    private bool _isOnRealWorld = true;
    private bool _isOnSight = false;

    void Start()
    {
        InvokeRepeating("Update", 3.0f, 1.0f);
    }

    void Update()
    {
        float currentMeter = 0.0f;
        if (_isOnSight)
        {
            currentMeter += _sightAmount;
        }
        currentMeter += _constantRate;
        _astralMeter = System.Math.Min(_maxMeter, _astralMeter + (currentMeter * Time.deltaTime));
    }

    public bool IsOnRealWorld()
    {
        return _isOnRealWorld;
    }

    public float GetAstralMeter()
    {
        return _astralMeter;
    }

    public float GetConstantRate()
    {
        return _constantRate;
    }

    public void ChangeWorld()
    {
        _isOnRealWorld = !_isOnRealWorld;
        if (_isOnRealWorld)
        {
            _constantRate = 0.05f;
        }
        else
        {
            _constantRate = 0.083f;
        }
    }

    public bool IsOnSight()
    {
        return _isOnSight;
    }

    public void ChangeSightState()
    {
        _isOnSight = !_isOnSight;
    }

    public void NPCWrongAnswer()
    {
        int randAnswerAmount = Random.Range(10, 15);
        _astralMeter += (float)randAnswerAmount;
    }

    public void PlayerKilled()
    {
        int randKillAmount = Random.Range(15, 20);
        _astralMeter += (float)randKillAmount;
    }
}
