using System.Collections;
using System.Collections.Generic;
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
    private bool _isUsed = false;
    private bool _isExorcised = false;

    public static event Action<string> FinishExorcismChannelingEvent;
    #endregion

    #region Enable - Disable
    private void OnEnable()
    {
        ExorcismInputHandler.UseReleasedEvent += StopExorcism;
    }

    private void OnDisable()
    {
        ExorcismInputHandler.UseReleasedEvent -= StopExorcism;
    }
    #endregion

    #region Update
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

    #region Setter Getter
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
    #endregion

    #region Exorcism Logic
    public void ShowBar(bool isActive)
    {
        slider.gameObject.SetActive(isActive);
    }

    public void StopExorcism()
    {
        _isUsed = false;
        ShowBar(false);
        FinishExorcismChannelingEvent?.Invoke("Player");
        ProcessExorcism();
    }

    public void ProcessExorcism()
    {
        if (_isExorcised)
        {
            //Debug.Log("[EXORCISM] Exorcism Finished");
        }
        else
        {
            //Debug.Log("[EXORCISM] Exorcism Cancelled");
        }
        _accumulatedTime = 0f;
    }
    #endregion
}
