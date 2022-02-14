using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IExorcismBar
{
    float GetSliderValue();
    void SetSliderValue(float hold);
    void SetSliderMinValue(float hold);
    void ShowBar(bool _isActive);
}


/*
 * Class to show Exorcism channeling on HUD.
 * Use Setter and Getter to access the variables.
 * Is referenced in ExorcismItem.
 */

public class ExorcismBar : MonoBehaviour, IExorcismBar
{
    #region Variable
    public Slider slider;
    #endregion

    #region Getter
    public float GetSliderValue()
    {
        return slider.value;
    }
    #endregion

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
}
