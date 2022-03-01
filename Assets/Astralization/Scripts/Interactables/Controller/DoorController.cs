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
    #endregion

    #region Variables
    [Header("Door States")]
    [SerializeField]
    [Tooltip("True if door is in open state")]
    private bool _isOpen = false;

    [Space]
    [SerializeField]
    [Header("Animation")]
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
        //Debug.Log("[INTERACTABLE] " + (isOpen ? "Closing " : "Opening ") + this.name);
        _isOpen = !_isOpen;
        _animator.SetBool(_animParam, _isOpen);
    }
    #endregion

    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
    }
    #endregion

    #region Handler
    public override void OnInteraction()
    {
        ChangeState();
        if (_isOpen) PlayAudio("Door_Open");
        else PlayAudio("Door_Close");
    }
    #endregion

    //TODO: Half Open for Ghost Interaction
}
