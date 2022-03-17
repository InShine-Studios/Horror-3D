using System;
using UnityEngine;
using UnityEngine.UI;

/*
 * Class to control Hiding Overlay
 */
public class HidingOverlay : MonoBehaviour
{
    #region Events
    public static event Action FinishHiding;
    public static event Action FinishUnhiding;
    #endregion

    #region Constants
    private const string _animParam = "isHiding";
    #endregion

    #region Variables
    [SerializeField]
    [Header("Animation")]
    private Animator _animator;

    private Image _overlay;
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        _overlay = transform.Find("Overlay").GetComponent<Image>();
    }
    #endregion

    #region Handler
    public void StartAnim(bool isHiding)
    {
        ChangeColor();
        _animator.SetBool(_animParam, isHiding);
    }

    private void ChangeColor()
    {
        _overlay.color = RenderSettings.fogColor;
    }

    private void OnFinishHiding()
    {
        FinishHiding?.Invoke();
    }

    private void OnFinishUnhiding()
    {
        FinishUnhiding?.Invoke();
    }
    #endregion
}
