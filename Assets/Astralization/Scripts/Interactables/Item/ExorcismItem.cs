using System;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IExorcismItem
{
    void StopUse();
    void ProcessExorcism();
    float GetAccumulatedTime();
    bool IsUsed();
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
    private float _sliderMinValue = 0f;
    private float _holdTime = 5.0f;
    private bool _isUsed = false;
    private bool _isExorcised = false;

    [SerializeField]
    private ExorcismBar _exorcismBar;
    private string _playerActionMap = "Exorcism";

    public static event Action<string> ExorcismChannelingEvent;
    public static event Action<float> ExorcismUpdateSliderEvent;
    public static event Action<float> ExorcismSetMinSliderEvent;
    #endregion

    protected override void Awake()
    {
        base.Awake();
        ExorcismSetMinSliderEvent?.Invoke(_sliderMinValue);
    }

    #region Update
    private void Update()
    {
        if (_isUsed)
        {
            ExorcismChannelingEvent?.Invoke(_playerActionMap);
            _accumulatedTime += Time.deltaTime;
            ExorcismUpdateSliderEvent?.Invoke(_accumulatedTime);
            if (_accumulatedTime >= _holdTime)
            {
                _isUsed = false;
                _isExorcised = true;
                ProcessExorcism();
            }
        }
    }
    #endregion

    #region Setter Getter
    public float GetAccumulatedTime()
    {
        return _accumulatedTime;
    }

    public bool IsUsed()
    {
        return _isUsed;
    }
    #endregion

    #region Exorcism Item Logic
    public override void Use()
    {
        _isUsed = true;
    }

    public override void StopUse()
    {
        if (_isUsed)
        {
            _isUsed = false;
            ProcessExorcism();
        }
    }

    public void ProcessExorcism()
    {
        if (_isExorcised)
        {
            //Debug.Log("[EXORCISM] Exorcism Finished");
            _exorcismBar.ShowBar(false);
            ExorcismChannelingEvent?.Invoke("Player");
        }
        else
        {
            ExorcismChannelingEvent?.Invoke("Player");
            //Debug.Log("[EXORCISM] Exorcism Cancelled");
            _exorcismBar.ShowBar(false);
        }
        _accumulatedTime = 0f;
    }
    #endregion
}
