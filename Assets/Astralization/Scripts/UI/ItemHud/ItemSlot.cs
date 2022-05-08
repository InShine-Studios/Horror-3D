using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    #region Constants
    private const float DefaultRadius = 100f;
    #endregion

    #region Variables
    private GameObject _circleActive;
    private GameObject _circleInactive;
    private RectTransform _rectTransform;
    private Text _quickslotNumber;
    private Image _itemImage;
    private static float _xScale;
    #endregion

    #region SetGet
    public void SetQuickslotNum(int num)
    {
        _quickslotNumber.text = num.ToString();
    }
    public void SetItemImage(Sprite sprite)
    {
        _itemImage.sprite = sprite;
        _itemImage.enabled = sprite != null;
    }
    public Image GetItemImage()
    {
        return _itemImage;
    }
    public void SetImageOpacity(float alpha)
    {
        Color newColorImage = _itemImage.color;
        newColorImage.a = alpha;
        _itemImage.color = newColorImage;
    }
    public void SetSelected(bool isSelected)
    {
         _circleActive.SetActive(isSelected);
        _circleInactive.SetActive(!isSelected);
        if (isSelected)
        {
            SetImageOpacity(1f);
        }
        else
        {
            SetImageOpacity(0.5f);
        }
    }
    public static float GetScaledRadius()
    {
        return DefaultRadius * _xScale;
    }
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        _circleActive = transform.Find("CircleActive").gameObject;
        _circleInactive = transform.Find("CircleInactive").gameObject;
        _rectTransform = GetComponent<RectTransform>();
        _quickslotNumber = GetComponentInChildren<Text>();
        _itemImage = GetComponentInChildren<Image>();
        _xScale = GetComponent<RectTransform>().localScale.x;
        SetItemImage(null);
        SetSelected(false);
    }
    #endregion

}
