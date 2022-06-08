using System;
using UnityEngine;

/*
 * Class to control closets door states.
 * Inherit Interactable.
 */
public class ClosetController : Interactable
{
    #region Events
    public static event Action StartHidingEvent;
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
        StartHidingEvent?.Invoke();
    }
    #endregion
}
