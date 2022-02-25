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
    public void UpdateLogo(bool state, Sprite logo)
    {
        _img.sprite = logo;
        _img.enabled = state;
    }
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        _img = transform.Find("Logo").GetComponent<Image>();
    }
    #endregion
}
