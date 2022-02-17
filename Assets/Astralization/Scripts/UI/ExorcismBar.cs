using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public interface IExorcismBar
{
    float GetAccumulatedTime();
    float GetSliderValue();
    bool IsUsed();
    void ProcessExorcism();
    void SetSliderMinValue(float hold);
    void SetSliderValue(float hold);
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

    public static event Action<float> ExorcismUpdateSliderEvent;
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

    public float GetSliderValue()
    {
        return slider.value;
    }
    #endregion

    #region Exorcism Logic
    public void ShowBar(bool isActive)
    {
        if (isActive)
        {
            slider.gameObject.SetActive(true);
        }
        else
        {
            slider.gameObject.SetActive(false);
        }
    }

    public void SetSliderMinValue(float hold)
    {
        slider.minValue = hold;
        slider.value = hold;
    }

    public void SetSliderValue(float hold)
    {
        slider.value = hold;
    }

    public void StopExorcism()
    {
        if (slider.gameObject.activeSelf)
        {
            ProcessExorcism();
        }
    }

    public void ProcessExorcism()
    {
        if (_isExorcised)
        {
            //Debug.Log("[EXORCISM] Exorcism Finished");
            ShowBar(false);
            FinishExorcismChannelingEvent?.Invoke("Player");
        }
        else
        {
            FinishExorcismChannelingEvent?.Invoke("Player");
            //Debug.Log("[EXORCISM] Exorcism Cancelled");
            ShowBar(false);
        }
        _accumulatedTime = 0f;
    }
    #endregion
}
