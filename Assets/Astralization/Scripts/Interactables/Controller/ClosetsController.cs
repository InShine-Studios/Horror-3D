using System;
using UnityEngine;

/*
 * Class to control closets door states.
 * Inherit Interactable.
 */
public class ClosetsController : Interactable
{
    #region Variables
    [Header("Door States")]
    [SerializeField]
    [Tooltip("True if door is in open state")]
    private bool _isHiding = false;

    public static event Action<bool> HidingAnim;
    public static event Action<Interactable, bool> MovePlayer;

    #endregion

    protected override void Awake()
    {
        base.Awake();
    }

    // General function to change the state of closets
    private void ChangeState()
    {
        _isHiding = !_isHiding;
        HidingAnim?.Invoke(_isHiding);
    }

    private void MovingPlayer(bool state)
    {
        MovePlayer?.Invoke(this, state);
    }

    public override void OnInteraction()
    {
        //Debug.Log("[INTERACTABLE] " + this.name + " interacted");
        ChangeState();
        MovingPlayer(_isHiding);
    }
}
