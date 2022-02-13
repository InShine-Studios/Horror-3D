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
    private string _hidingActionMap = "Hiding";

    #endregion

    protected override void Awake()
    {
        base.Awake();
    }

    public override void OnInteraction()
    {
        //Debug.Log("[INTERACTABLE] " + this.name + " interacted");
        StartHidingEvent?.Invoke(_hidingActionMap);
    }
}
