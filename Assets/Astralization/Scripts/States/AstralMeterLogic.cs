using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAstralMeterLogic
{
    void ChangeSightState();
    float GetAstralMeter();
    float GetConstantRate();
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
    private bool _isOnSight = false;

    void Start()
    {
        InvokeRepeating("Increment", 3.0f, 1.0f);
    }

    void Increment()
    {
        float currentMeter = 0.0f;
        if (_isOnSight)
        {
            currentMeter += _sightAmount;
        }
        currentMeter += _constantRate;
        _astralMeter = System.Math.Min(_maxMeter, _astralMeter + (currentMeter * Time.deltaTime));
    }

    private void OnEnable()
    {
        GameManager.ChangeWorldEvent += ChangeWorld;
    }

    private void OnDisable()
    {
        GameManager.ChangeWorldEvent -= ChangeWorld;
    }

    private void ChangeWorld(bool state)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject go = transform.GetChild(i).gameObject;
            go.SetActive(state);
        }
        ToggleWorldRate(state);
    }

    private void ToggleWorldRate(bool state)
    {
        if (state)
        {
            _constantRate = 0.083f;
        }
        else _constantRate = 0.05f;
    }

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
