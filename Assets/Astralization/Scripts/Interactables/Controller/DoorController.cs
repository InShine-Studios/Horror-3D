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
    #region Variables
    private const string _animParam = "isOpen";
    [Header("Door States")]
    [SerializeField]
    [Tooltip("True if door is in open state")]
    private bool _isOpen = false;

    [Space]
    [SerializeField]
    [Header("Animation")]
    private Animator _animator;
    #endregion

    protected override void Awake()
    {
        base.Awake();
    }

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

    public override void OnInteraction()
    {
        //Debug.Log("[INTERACTABLE] Door interacted");
        ChangeState();
        //if (_isOpen) PlayAudio("Door_Open");
        //else PlayAudio("Door_Close");
    }

    //TODO: Half Open for Ghost Interaction

}
