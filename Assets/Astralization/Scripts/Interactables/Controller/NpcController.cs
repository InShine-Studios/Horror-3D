using System;
using UnityEngine;

public interface INpcController
{
    bool GetOneFinished();
    bool GetTwoFinished();
}


/*
 * Class to control Npc states.
 * Inherit Interactable.
 */
public class NpcController : Interactable, INpcController
{
    #region Variables
    private bool _questOneFinished;
    private bool _questTwoFinished;

    public static event Action NpcInteractionEvent;
    #endregion

    #region Setter Getter
    public bool GetOneFinished()
    {
        return _questOneFinished;
    }

    public bool GetTwoFinished()
    {
        return _questTwoFinished;
    }
    #endregion

    public override void OnInteraction()
    {
        //Debug.Log("[INTERACTABLE] Npc interacted: " + this.name);
        NpcInteractionEvent?.Invoke();
    }
}
