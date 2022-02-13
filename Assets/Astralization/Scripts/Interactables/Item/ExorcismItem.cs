using System;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IExorcismItem
{
    void ButtonReleased();
    void ExorcismOngoing();
    float GetAccTime();
    bool GetUsed();
    void Use();
}

/*
 * Class to use Exorcism Item.
 * Use Setter and Getter to access the variables.
 * HUD managed by ExorcismBar.
 */

public class ExorcismItem : Item, IExorcismItem
{
    #region Variables

    private float _accumulatedTime = 0f;
    private float _minHold = 0f;
    private float _holdTime = 5.0f;
    private bool _isUsed = false;

    [SerializeField]
    private ExorcismBar _exorcismBar;
    private string _playerActionMap = "Exorcism";

    public static event Action<string> ExorcismChannelingEvent;
    #endregion

    #region Update - Awake
    private void Update()
    {
        if (_isUsed)
        {
            /*Debug.Log("[EXORCISM] Item used");*/
            ExorcismChannelingEvent?.Invoke(_playerActionMap);
            _exorcismBar.SetMinHold(_minHold);
            _accumulatedTime += Time.deltaTime;
            //Debug.Log("[EXORCISM BAR] Accumulated Time = " + _accumulatedTime);
            _exorcismBar.SetHold(_accumulatedTime);
            //Debug.Log("[EXORCISM BAR] Bar = " + _exorcismBar.slider.value);
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

    #region Getter
    public float GetAccTime()
    {
        return _accumulatedTime;
    }

    public bool GetUsed()
    {
        return _isUsed;
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
            _exorcismBar.ShowBar(false);
            ExorcismChannelingEvent?.Invoke("Player");
        }
        else
        {
            ExorcismChannelingEvent?.Invoke("Player");
            Debug.Log("[EXORCISM] Exorcism Cancelled");
            _exorcismBar.ShowBar(false);
        }
        _accumulatedTime = 0f;
    }
    #endregion
}
