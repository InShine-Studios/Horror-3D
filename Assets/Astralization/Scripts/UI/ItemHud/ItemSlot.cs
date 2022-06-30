using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    #region Constants
    private const float DefaultRadius = 100f;
    private const string AnimationParamName = "States";
    #endregion

    #region Variables
    private GameObject _circleActive;
    private GameObject _circleInactive;
    private RectTransform _rectTransform;
    private Text _quickslotNumber;
    private Image _itemImage;
    private Animator _animator;
    private static float _xScale;
    #endregion

    #region SetGet
    public void SetQuickslotNum(int num)
    {
        _quickslotNumber.text = num.ToString();
    }
    public void SetLogoAnimController(RuntimeAnimatorController animController)
    {
        _itemImage.enabled = animController != null;
        _animator.runtimeAnimatorController = animController;
    }

    public void SetLogoAnimParam(int animParam)
    {
        _animator.SetInteger(AnimationParamName, animParam);
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
        _animator = GetComponent<Animator>();
        SetSelected(false);
    }
    #endregion

}
