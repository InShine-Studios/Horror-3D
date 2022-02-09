using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Class to show Exorcism channeling on HUD.
 * Use Setter and Getter to access the variables.
 * Is referenced in ExorcismItem.
 */

public class ExorcismBar : MonoBehaviour
{
    public Slider slider;

    public void ShowBar()
    {
        this.gameObject.SetActive(true);
    }

    public void HideBar()
    {
        this.gameObject.SetActive(false);
    }

    public void SetMinHold (float hold)
    {
        slider.minValue = hold;
        slider.value = hold;
    }

    public void SetHold (float hold)
    {
        slider.value = hold;
    }
}
