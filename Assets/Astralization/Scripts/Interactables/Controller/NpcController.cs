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

    //[Space]
    //[Header("Player")]
    //[Tooltip("Player Prefab")]
    //private Light[] _player;
    #endregion

    private void Awake()
    {
        // TODO isi kalo butuh
    }
    public override void OnInteraction()
    {
        _isInteracted = !_isInteracted;
        //if(_isInteracted) PlayAudio("Switch_On");
        //else PlayAudio("Switch_Off");
        Debug.Log(
            ("[INTERACTABLE] "+ this.name + (_isInteracted ? "on" : "off"))
        );
    }

    private void SetPlayerInputState(bool value)
    {
        // TODO disable input system
    }

    public bool GetState()
    {
        return _isInteracted;
    }
}
