using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
