using System;
using UnityEngine;

public interface IDoorController: IInteractable
{
    bool GetState();
    float GetAngle();
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

    // General function to change the state of doors
    private void ChangeState()
    {
        if (!IsTransitioning())
        {
            //Debug.Log("[INTERACTABLE] " + (isOpen ? "Closing " : "Opening ") + this.name);
            _isOpen = !_isOpen;
            _animator.SetBool(_animParam, _isOpen);
            if (_isOpen) PlayAudio("Door_Open");
            else PlayAudio("Door_Close");
        }
    }

    public bool IsTransitioning()
    {
        bool isTransitioning = false;
        foreach (string transitionAnimName in _transitionAnimNames)
        {
            isTransitioning |= _animator.GetCurrentAnimatorStateInfo(0).IsName(transitionAnimName);
        }
        return isTransitioning;
    }
    #endregion

    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        _animator = GetComponentInParent<Animator>();
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
