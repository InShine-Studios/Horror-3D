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

    #region Enable - Disable
    private void OnEnable()
    {
        Inventory.ItemLogoEvent += UpdateLogo;
    }

    private void OnDisable()
    {
        Inventory.ItemLogoEvent -= UpdateLogo;
    }
    #endregion

    public void UpdateLogo(bool state, Sprite logo)
    {
        _img.sprite = logo;
        _img.enabled = state;
    }
}
