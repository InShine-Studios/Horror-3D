using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ExorcismItem : Item
{
    #region Variables
    public static event Action Exorcism;

    private float _accumulatedTime = 0f;
    private float _holdTime = 5.0f;
    private bool _isUsed = false;
    #endregion

    private void Update()
    {
        if (_isUsed)
        {
            /*Debug.Log("[EXORCISM] Item used");*/
            _accumulatedTime += Time.deltaTime;
            if (_accumulatedTime >= _holdTime)
            {
                _isUsed = false;
                ExorcismOngoing();
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();
    }

    public override void Use()
    {
        _isUsed = true;
    }

    public override void ButtonReleased()
    {
        if (_isUsed)
        {
            _isUsed = false;
            ExorcismOngoing();
        }
    }

    public void ExorcismOngoing()
    {
        if (_accumulatedTime >= _holdTime)
        {
            Debug.Log("[EXORCISM] Exorcism Finished");
        }
        else
        {
            Debug.Log("[EXORCISM] Exorcism Cancelled");
        }
        _accumulatedTime = 0f;
    }
}
