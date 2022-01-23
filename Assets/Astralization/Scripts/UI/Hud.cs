using UnityEngine;
using UnityEngine.UI;

/*
 * Class to control HUD.
 */
public class Hud : MonoBehaviour
{
    private Image _img;

    private void Awake()
    {
        _img = transform.Find("Canvas/Placeholder").GetComponent<Image>();
    }
    public void ActivateHud(Sprite logo)
    {
        _img.enabled = true;
        _img.sprite = logo;
    }
    public void DeactivateHud()
    {
        _img.enabled = false;
        _img.sprite = null;
    }
}
