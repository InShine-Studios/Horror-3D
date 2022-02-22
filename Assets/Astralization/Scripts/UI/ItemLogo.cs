using UnityEngine;
using UnityEngine.UI;

/*
 * Class to control Item HUD
 */
public class ItemLogo : MonoBehaviour
{
    private Image _img;

    private void Awake()
    {
        _img = transform.Find("Logo").GetComponent<Image>();
    }

    public void UpdateLogo(bool state, Sprite logo)
    {
        _img.sprite = logo;
        _img.enabled = state;
    }
}
