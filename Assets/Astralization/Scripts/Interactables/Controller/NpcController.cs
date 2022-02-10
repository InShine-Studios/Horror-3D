using System;

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
    private string _dialogueActionMap = "Dialogue";

    public static event Action<string> NpcInteractionEvent;
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
        //Debug.Log("[INTERACTABLE] Action map: " + _dialogueActionMap);
        NpcInteractionEvent?.Invoke(_dialogueActionMap);
    }
}
