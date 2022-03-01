using System;
using UnityEngine;
using UnityEngine.UI;

public interface IExorcismBar
{
    float GetAccumulatedTime();
    float GetSliderValue();
    bool IsExorcised();
    bool IsUsed();
    void ProcessExorcism();
    void SetSliderMinValue(float sliderValue);
    void SetSliderValue(float sliderValue);
    void ShowBar(bool isActive);
    void StopExorcism();
}

/*
 * Class to show Exorcism channeling on HUD.
 * Slider will show on screen during exorcism channeling.
*/
public class ExorcismBar : MonoBehaviour, IExorcismBar
{
    #region Variable
    public Slider slider;

    [SerializeField]
    private float _accumulatedTime = 0f;
    [SerializeField]
    private float _holdTime = 5.0f;
    [SerializeField]
    private float _minValue = 0.0f;
    private bool _isUsed = false;
    private bool _isExorcised = false;

    public static event Action FinishExorcismChannelingEvent;
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        SetSliderMinValue(_minValue);
    }

    private void OnEnable()
    {
        ExorcismState.UseReleasedEvent += StopExorcism;
    }

    private void OnDisable()
    {
        ExorcismState.UseReleasedEvent -= StopExorcism;
    }

    private void Update()
    {
        if (slider.gameObject.activeSelf)
        {
            _isUsed = true;
            _accumulatedTime += Time.deltaTime;
            SetSliderValue(_accumulatedTime);
            if (_accumulatedTime >= _holdTime)
            {
                _isExorcised = true;
                StopExorcism();
            }
        }
    }
    #endregion

    #region SetGet
    public float GetAccumulatedTime()
    {
        return _accumulatedTime;
    }

    public bool IsUsed()
    {
        return _isUsed;
    }

    public float GetSliderValue()
    {
        return slider.value;
    }

    public bool IsExorcised()
    {
        return _isExorcised;
    }

    public void SetSliderMinValue(float sliderValue)
    {
        slider.minValue = sliderValue;
        slider.value = sliderValue;
    }

    public void SetSliderValue(float sliderValue)
    {
        slider.value = sliderValue;
    }

    public void ShowBar(bool isActive)
    {
        slider.gameObject.SetActive(isActive);
    }
    #endregion

    #region Handler
    public void StopExorcism()
    {
        _isUsed = false;
        ShowBar(false);
        FinishExorcismChannelingEvent?.Invoke();
        ProcessExorcism();
    }

    public void ProcessExorcism()
    {
        if (_isExorcised)
        {
            //Debug.Log("[HUD SYSTEM] Exorcism Finished");
        }
        else
        {
            //Debug.Log("[HUD SYSTEM] Exorcism Cancelled");
        }
        _accumulatedTime = 0f;
    }
    #endregion
}
