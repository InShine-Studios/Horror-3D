using System;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IExorcismItem
{
    void ButtonReleased();
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
    #endregion

    protected override void Awake()
    {
        base.Awake();
    }

    #region Update
    private void Update()
    {
        if (_isUsed)
        {
            /*Debug.Log("[EXORCISM] Item used");*/
            ExorcismChannelingEvent?.Invoke(_playerActionMap);
            _exorcismBar.SetSliderMinValue(_sliderMinValue);
            _accumulatedTime += Time.deltaTime;
            //Debug.Log("[EXORCISM BAR] Accumulated Time = " + _accumulatedTime);
            ExorcismUpdateSliderEvent?.Invoke(_accumulatedTime);

            //Debug.Log("[EXORCISM BAR] Bar = " + _exorcismBar.slider.value);
            if (_accumulatedTime >= _holdTime)
            {
                _isUsed = false;
                _isExorcised = true;
                ProcessExorcism();
            }
            //ButtonReleased();
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

    public override void ButtonReleased()
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
