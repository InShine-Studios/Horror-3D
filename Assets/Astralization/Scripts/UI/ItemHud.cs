using UnityEngine;
using UnityEngine.UI;

/*
 * Class to control Item HUD
 */
public class ItemHud : MonoBehaviour
{
    private Image _img;

    private void Awake()
    {
        _img = transform.Find("Logo").GetComponent<Image>();
    }
    public void ActivateHud(Sprite logo)
    {
        _img.sprite = logo;
        _img.enabled = true;
    }
    public void DeactivateHud()
    {
        _img.sprite = null;
        _img.enabled = false;
    }
}
