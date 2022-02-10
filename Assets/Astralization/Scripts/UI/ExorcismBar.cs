using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IExorcismBar
{
    float GetSliderValue();
    void HideBar();
    void SetHold(float hold);
    void SetMinHold(float hold);
    void ShowBar();
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

    public float GetSliderValue()
    {
        return slider.value;
    }
    
    public void ShowBar()
    {
        this.gameObject.SetActive(true);
    }

    public void HideBar()
    {
        this.gameObject.SetActive(false);
    }

    public void SetMinHold(float hold)
    {
        slider.minValue = hold;
        slider.value = hold;
    }

    public void SetHold(float hold)
    {
        slider.value = hold;
    }
}
