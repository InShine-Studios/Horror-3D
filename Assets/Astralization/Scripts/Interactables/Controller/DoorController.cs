using System;
using UnityEngine;

public interface IDoorController: IInteractable
{
    bool GetState();
    float GetAngle();
    void SetIsTransitioning(bool isTransitioning);
}

/*
 * Class to control door states and animation.
 * Inherit Interactable.
 */
public class DoorController : Interactable, IDoorController
{
    #region Constants
    private const string _animParam = "isOpen";
    private static readonly string[] _transitionAnimNames = { "Opening", "Closing" };
    #endregion

    #region Variables
    [Header("Door States")]
    [SerializeField]
    [Tooltip("True if door is in open state")]
    private bool _isOpen = false;
    [Tooltip("True if door is in open state")]
    private bool _isTransitioning = true;

    private Animator _animator;
    #endregion

    #region SetGet
    public bool GetState()
    {
        return _isOpen;
    }

    public float GetAngle()
    {
        return transform.parent.rotation.y;
    }

    public void SetIsTransitioning(bool isTransitioning)
    {
        _isTransitioning = isTransitioning;
    }

    // General function to change the state of doors
    private void ChangeState()
    {
        if (_isTransitioning)
        {
            //Debug.Log("[INTERACTABLE] " + (isOpen ? "Closing " : "Opening ") + this.name);
            _isOpen = !_isOpen;
            _animator.SetBool(_animParam, _isOpen);
            if (_isOpen) PlayAudio("Door_Open");
            else PlayAudio("Door_Close");
        }
    }
    #endregion

    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        _animator = GetComponentInParent<Animator>();
        _isTransitioning = true;
    }
    #endregion

    #region Handler
    public override void OnInteraction()
    {
        ChangeState();
    }
    #endregion

    //TODO: Half Open for Ghost Interaction
}
