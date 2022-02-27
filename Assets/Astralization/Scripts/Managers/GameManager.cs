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
    public static event Action<InputManager.States> PlayerStateEvent;
    public static event Action<HudManager.States, bool> HudEvent;
    // TODO: to be implemented
    public static event Action PlayerAudioDiesEvent;
    #endregion

    #region Enable - Disable
    private void OnEnable()
    {
        AnkhItem.ChangeWorldGM += InvokeChangeWorld;
        NpcController.NpcInteractionEvent += InvokeDialogueState;
        DialogueManager.FinishDialogueEvent += ResetPlayerState;
        ClosetsController.StartHidingEvent += InvokeHidingState;
        ExorcismItem.ExorcismChannelingEvent += InvokeExorcismState;
        ExorcismBar.FinishExorcismChannelingEvent += ResetPlayerState;
        HideInputHandler.StopHidingEvent += ResetPlayerState;
    }

    private void OnDisable()
    {
        AnkhItem.ChangeWorldGM -= InvokeChangeWorld;
        NpcController.NpcInteractionEvent -= InvokeDialogueState;
        DialogueManager.FinishDialogueEvent -= ResetPlayerState;
        ClosetsController.StartHidingEvent -= InvokeHidingState;
        ExorcismItem.ExorcismChannelingEvent -= InvokeExorcismState;
        ExorcismBar.FinishExorcismChannelingEvent -= ResetPlayerState;
        HideInputHandler.StopHidingEvent -= ResetPlayerState;
    }
    #endregion

    #region Setter Getter
    public bool IsInAstralWorld() { return _isInAstralWorld; }
    #endregion

    #region SendEvents
    public void SendHudEvent(HudManager.States hudKey, bool conditions)
    {
        HudEvent?.Invoke(hudKey, conditions);
    }

    public void SendPlayerStateEvent(InputManager.States actionMapKey)
    {
        PlayerStateEvent?.Invoke(actionMapKey);
    }
    #endregion

    #region InvokeFunctions
    public void InvokeChangeWorld()
    {
        _isInAstralWorld = !_isInAstralWorld;
        ChangeWorldEvent?.Invoke(_isInAstralWorld);
    }

    public void InvokeDialogueState()
    {
        SendHudEvent(HudManager.States.Dialogue, true);
        SendPlayerStateEvent(InputManager.States.Dialogue);
    }

    public void InvokeHidingState()
    {
        SendHudEvent(HudManager.States.Hiding, true);
        SendPlayerStateEvent(InputManager.States.Hiding);
    }

    public void InvokeExorcismState()
    {
        SendHudEvent(HudManager.States.Exorcism, true);
        SendPlayerStateEvent(InputManager.States.Exorcism);
    }

    public void ResetPlayerState()
    {
        //Debug.Log("[INVOKE PLAYER STATE] Player state: " + state);
        SendPlayerStateEvent(InputManager.States.Default);
    }
    #endregion
}
