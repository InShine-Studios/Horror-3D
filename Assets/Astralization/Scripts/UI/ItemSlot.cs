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
    #endregion

    #region SetGet
    public void SetPositionOnCanvas(Vector3 offset, Vector3 gap, int index)
    {
        _rectTransform.localPosition = offset + ((gap + new Vector3(GetScaledRadius(),0,0)) * index);
    }
    public void SetQuickslotNum(int num)
    {
        _quickslotNumber.text = num.ToString();
    }
    public void SetItemImage(Sprite sprite)
    {
        _itemImage.sprite = sprite;
        _itemImage.enabled = sprite != null;
    }
    public void SetImageOpacity(float alpha)
    {
        Color newColorImage = _itemImage.color;
        newColorImage.a = alpha;
        _itemImage.color = newColorImage;
    }
    public void SetActive(bool isActive)
    {
        Debug.Log(name);
        _circleActive.SetActive(isActive);
        _circleInactive.SetActive(!isActive);
        if (isActive)
        {
            SetImageOpacity(1f);
        }
        else
        {
            SetImageOpacity(0.5f);
        }
    }
    public float GetScaledRadius()
    {
        return DefaultRadius * GetComponent<RectTransform>().localScale.x;
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
        SetItemImage(null);
        SetActive(false);
    }
    #endregion
}
