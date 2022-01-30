using System;
using UnityEngine;

public interface INpcController: IInteractable
{
    bool GetState();
}

/*
 * Class to control Npc states.
 * Inherit Interactable.
 */
public class NpcController : Interactable, INpcController
{
    #region Variables
    [Header("NPC state")]
    [SerializeField]
    [Tooltip("True if NPC is interacted")]
    private bool _isInteracted = false;

    public static event Action<bool> NpcInteractionEvent;
    #endregion

    private void Awake()
    {
        // TODO isi kalo butuh
    }
    public override void OnInteraction()
    {
        _isInteracted = !_isInteracted;
        //Debug.Log("OnInteraction: " + _isInteracted);
        if (_isInteracted)
            NpcInteractionEvent?.Invoke(true);
        else
            NpcInteractionEvent?.Invoke(false);
        //else PlayAudio("Switch_Off");
        //Debug.Log(
        //    ("[INTERACTABLE] "+ this.name + (_isInteracted ? "on" : "off"))
        //);
    }

    public bool GetState()
    {
        return _isInteracted;
    }
}
