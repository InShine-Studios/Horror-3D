using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ExorcismItem : Item
{
    #region Variables

    private float _accumulatedTime = 0f;
    private float _minHold = 0f;
    private float _holdTime = 5.0f;
    private bool _isUsed = false;

    [SerializeField]
    private ExorcismBar _exorcismBar;
    [SerializeField]
    private GameObject _exorcismHud;
    #endregion

    #region Update - Awake

    private void Update()
    {
        if (_isUsed)
        {
            /*Debug.Log("[EXORCISM] Item used");*/
            _exorcismHud.SetActive(true);
            _exorcismBar.SetMinHold(_minHold);
            _accumulatedTime += Time.deltaTime;
            //Debug.Log("[EXORCISM BAR] Accumulated Time = " + _accumulatedTime);
            _exorcismBar.SetHold(_accumulatedTime);
            Debug.Log("[EXORCISM BAR] Bar = " + _exorcismBar.slider.value);
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
    #endregion

    #region Exorcism Item Logic
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
            _exorcismHud.SetActive(false);
        }
        else
        {
            Debug.Log("[EXORCISM] Exorcism Cancelled");
            _exorcismHud.SetActive(false);
        }
        _accumulatedTime = 0f;
    }
    #endregion
}
