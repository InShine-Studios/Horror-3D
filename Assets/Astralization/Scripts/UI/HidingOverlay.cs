using UnityEngine;
using UnityEngine.UI;

public class HidingOverlay : MonoBehaviour
{
    // Start is called before the first frame update
    #region Variables
    private const string _animParam = "isHiding";

    [Space]
    [SerializeField]
    [Header("Animation")]
    private Animator _animator;

    private Image _overlay;
    #endregion

    private void Awake()
    {
        _overlay = transform.Find("Overlay").GetComponent<Image>();
    }

    public void StartAnim(bool isHiding)
    {
        ChangeColor();
        _animator.SetBool(_animParam, isHiding);
    }

    private void ChangeColor()
    {
        _overlay.color = RenderSettings.fogColor;
    }
}
