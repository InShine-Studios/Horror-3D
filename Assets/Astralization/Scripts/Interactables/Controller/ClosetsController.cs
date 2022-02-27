using System;
using UnityEngine;

/*
 * Class to control closets door states.
 * Inherit Interactable.
 */
public class ClosetsController : Interactable
{
    #region Constants
    private string _hidingActionMap = "Hiding";
    #endregion

    #region Events
    public static event Action<string> StartHidingEvent;
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
        //Debug.Log("[INTERACTABLE] " + this.name + " interacted");
        StartHidingEvent?.Invoke(_hidingActionMap);
    }
    #endregion
}
