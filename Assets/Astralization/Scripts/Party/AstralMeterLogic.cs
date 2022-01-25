using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAstralMeterLogic
{
    void ChangeSight();
    void ChangeWorld();
    float GetAstralMeter();
    float GetConstantRate();
    void NPCWrongAnswer();
    bool OnSight();
    void PlayerKilled();
    bool World();
}

public class AstralMeterLogic : MonoBehaviour, IAstralMeterLogic
{
    private float _astralMeter = 0.0f;
    private float _maxMeter = 100.0f;
    private float _constantRate = 0.05f;
    private float sightAmount = 1.0f;
    public bool isOnRealWorld = true;
    public bool isOnSight = false;

    // Start is called before the first frame update
    private void Start()
    {

    }

    public bool World()
    {
        return isOnRealWorld;
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
        isOnRealWorld = !isOnRealWorld;
        if (isOnRealWorld)
        {
            _constantRate = 0.05f;
        }
        else
        {
            _constantRate = 0.083f;
        }
    }

    public bool OnSight()
    {
        return isOnSight;
    }

    public void ChangeSight()
    {
        isOnSight = !isOnSight;
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

    // Update is called once per frame
    void Update()
    {
        float currentMeter = 0.0f;
        if (isOnSight)
        {
            currentMeter += sightAmount;
        }
        currentMeter += _constantRate;
        _astralMeter = System.Math.Min(_maxMeter, _astralMeter + (currentMeter * Time.deltaTime));
    }
}
