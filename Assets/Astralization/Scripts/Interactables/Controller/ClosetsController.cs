using System;
using UnityEngine;

/*
 * Class to control closets door states.
 * Inherit Interactable.
 */
public class ClosetsController : Interactable
{
    #region Variables

    public static event Action<string> StartHidingEvent;
    public static event Action<Interactable, bool> HidePlayer;
    public static event Action<bool> StopHidingHudEvent;
    public static event Action<string> StopHidingEvent;
    private string _hidingActionMap = "Hiding";
    private string _playerActionMap = "Player";
    private bool _isHiding = false;

    #endregion

    protected override void Awake()
    {
        base.Awake();
    }

    // General function to change the state of closets
    private void ChangeState()
    {
        _isHiding = !_isHiding;
        if(_isHiding) StartHidingEvent?.Invoke(_hidingActionMap);
        else
        {
            StopHidingEvent?.Invoke(_playerActionMap);
            StopHidingHudEvent?.Invoke(false);
        }
        HidePlayer?.Invoke(this, _isHiding);
    }

    public override void OnInteraction()
    {
        //Debug.Log("[INTERACTABLE] " + this.name + " interacted");
        ChangeState();
    }
}
