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

    public static event Action<bool> NpcInteractionEvent;
    #endregion

    public bool GetOneFinished()
    {
        return _questOneFinished;
    }

    public bool GetTwoFinished()
    {
        return _questTwoFinished;
    }

    public override void OnInteraction()
    {
        NpcInteractionEvent?.Invoke(true);
        //Debug.Log(
        //    ("[INTERACTABLE] "+ this.name)
        //);
    }
}
