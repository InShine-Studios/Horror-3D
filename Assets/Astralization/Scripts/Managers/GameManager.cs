using System;
using UnityEngine;

public interface IGameManager
{
    bool IsInAstralWorld();
    void InvokeChangeWorld();
}

public class GameManager : MonoBehaviour, IGameManager
{
    #region Variables
    [Tooltip("Bool flag to check if the player is in Real World or Astral World")]
    [SerializeField]
    private bool _isInAstralWorld = false;
    #endregion

    #region Event
    public static event Action<bool> ChangeWorldEvent;
    public static event Action<string> PlayerActionMapEvent;
    public static event Action<string, bool> HudMapEvent;
    // TODO: to be implemented
    public static event Action PlayerAudioDiesEvent;
    #endregion

    #region Enable - Disable
    private void OnEnable()
    {
        AnkhItem.ChangeWorldGM += InvokeChangeWorld;
        NpcController.NpcInteractionEvent += InvokeDialogueState;
        DialogueManager.FinishDialogueEvent += InvokePlayerState;
        ClosetsController.StartHidingEvent += InvokeHidingState;
        ExorcismItem.ExorcismChannelingEvent += InvokeExorcismState;
        ExorcismBar.FinishExorcismChannelingEvent += InvokePlayerState;
    }

    private void OnDisable()
    {
        AnkhItem.ChangeWorldGM -= InvokeChangeWorld;
        NpcController.NpcInteractionEvent -= InvokeDialogueState;
        DialogueManager.FinishDialogueEvent -= InvokePlayerState;
        ClosetsController.StartHidingEvent -= InvokeHidingState;
        ExorcismItem.ExorcismChannelingEvent -= InvokeExorcismState;
        ExorcismBar.FinishExorcismChannelingEvent -= InvokePlayerState;
    }
    #endregion

    #region Setter Getter
    public bool IsInAstralWorld() { return _isInAstralWorld; }
    #endregion

    #region Invoker
    public void InvokeChangeWorld()
    {
        _isInAstralWorld = !_isInAstralWorld;
        ChangeWorldEvent?.Invoke(_isInAstralWorld);
    }

    public void InvokeDialogueState()
    {
        HudMapEvent?.Invoke("Dialogue", true);
        PlayerActionMapEvent?.Invoke("Dialogue");
    }
    public void InvokeHidingState()
    {
        HudMapEvent?.Invoke("Hiding", true);
        PlayerActionMapEvent?.Invoke("Hiding");
    }

    public void InvokeExorcismState()
    {
        HudMapEvent?.Invoke("Exorcism", true);
        PlayerActionMapEvent?.Invoke("Exorcism");
    }

    public void InvokePlayerState()
    {
        //Debug.Log("[INVOKE PLAYER STATE] Player state: " + state);
        PlayerActionMapEvent?.Invoke("Default");
    }
    #endregion
}
