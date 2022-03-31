using System;
using UnityEngine;
using Utils;

public interface IGameManager
{
    void InvokeChangeWorld();
    void InvokeDialogueState();
    void InvokeExorcismState();
    void InvokeHidingState();
    void ResetPlayerState();
    void SendHudEvent(PlayerHelper.States hudKey, bool condition);
    void SendPlayerStateEvent(PlayerHelper.States actionMapKey);
}

public class GameManager : MonoBehaviour, IGameManager
{
    #region Event
    public static event Action ChangeWorldEvent;
    public static event Action<Utils.PlayerHelper.States> PlayerStateEvent;
    public static event Action<Utils.PlayerHelper.States, bool> HudEvent;
    // TODO: to be implemented
    public static event Action PlayerAudioDiesEvent;
    #endregion

    #region MonoBehaviour
    private void OnEnable()
    {
        AnkhItem.ChangeWorldGM += InvokeChangeWorld;
        NpcController.NpcInteractionEvent += InvokeDialogueState;
        DialogueManager.FinishDialogueEvent += ResetPlayerState;
        ClosetsController.StartHidingEvent += InvokeHidingState;
        ExorcismItem.ExorcismChannelingEvent += InvokeExorcismState;
        ExorcismBar.FinishExorcismChannelingEvent += ResetPlayerState;
        HidingState.StopHidingEvent += ResetPlayerState;
    }

    private void OnDisable()
    {
        AnkhItem.ChangeWorldGM -= InvokeChangeWorld;
        NpcController.NpcInteractionEvent -= InvokeDialogueState;
        DialogueManager.FinishDialogueEvent -= ResetPlayerState;
        ClosetsController.StartHidingEvent -= InvokeHidingState;
        ExorcismItem.ExorcismChannelingEvent -= InvokeExorcismState;
        ExorcismBar.FinishExorcismChannelingEvent -= ResetPlayerState;
        HidingState.StopHidingEvent -= ResetPlayerState;
    }
    #endregion

    #region SendEvents
    public void SendHudEvent(Utils.PlayerHelper.States hudKey, bool condition)
    {
        HudEvent?.Invoke(hudKey, condition);
    }

    public void SendPlayerStateEvent(Utils.PlayerHelper.States actionMapKey)
    {
        PlayerStateEvent?.Invoke(actionMapKey);
    }
    #endregion

    #region Invoker
    public void InvokeChangeWorld()
    {
        ChangeWorldEvent?.Invoke();
        //Debug.Log("[MANAGER] Changing world state to " + (_isInAstralWorld ? "Astral" : "Real"));
    }

    public void InvokeDialogueState()
    {
        SendHudEvent(Utils.PlayerHelper.States.Dialogue, true);
        SendPlayerStateEvent(Utils.PlayerHelper.States.Dialogue);
        //Debug.Log("[MANAGER] Change state to dialogue");
    }

    public void InvokeHidingState()
    {
        SendHudEvent(Utils.PlayerHelper.States.Hiding, true);
        SendPlayerStateEvent(Utils.PlayerHelper.States.Hiding);
        //Debug.Log("[MANAGER] Change state to hiding");
    }

    public void InvokeExorcismState()
    {
        SendHudEvent(Utils.PlayerHelper.States.Exorcism, true);
        SendPlayerStateEvent(Utils.PlayerHelper.States.Exorcism);
        //Debug.Log("[MANAGER] Change state to exorcism");
    }

    public void ResetPlayerState()
    {
        SendPlayerStateEvent(Utils.PlayerHelper.States.Default);
        //Debug.Log("[MANAGER] Reset player state to default");
    }
    #endregion
}
