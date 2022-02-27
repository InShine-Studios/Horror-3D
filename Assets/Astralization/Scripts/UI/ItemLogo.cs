using UnityEngine;
using UnityEngine.UI;

/*
 * Class to control Item HUD
 */
public class ItemLogo : MonoBehaviour
{
    #region Variables
    private Image _img;
    #endregion

    #region SetGet
    public void SetSpriteLogo(Sprite logo)
    {
        _img.sprite = logo;
    }
    
    public void ShowLogo(bool isShowing)
    {
        _img.enabled = isShowing;
    }
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        _img = transform.Find("Logo").GetComponent<Image>();
    }
    #endregion

    #region Updater
    public void UpdateLogo(bool state, Sprite logo)
    {
        SetSpriteLogo(logo);
        ShowLogo(state);
    }
    #endregion
}
