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
    void SendHudPlayerEvent(PlayerHelper.States hudKey, bool condition);
    void SendHudUiEvent(UiHelper.States hudKey, bool condition);
    void SendPlayerStateEvent(PlayerHelper.States actionMapKey);
}

public class GameManager : MonoBehaviour, IGameManager
{
    #region Event
    public static event Action ChangeWorldEvent;
    public static event Action<Utils.PlayerHelper.States> PlayerStateEvent;
    public static event Action<Utils.PlayerHelper.States, bool> HudPlayerEvent;
    public static event Action<Utils.UiHelper.States, bool> HudUiEvent;
    // TODO: to be implemented
    public static event Action PlayerAudioDiesEvent;
    #endregion

    #region MonoBehaviour
    private void OnEnable()
    {
        AnkhItem.ChangeWorldGM += InvokeChangeWorld;
        GhostManager.ChangeWorldGM += InvokeChangeWorld;
        VictimController.VictimInteractionEvent += InvokeDialogueState;
        DialogueManager.FinishDialogueEvent += ResetPlayerState;
        ClosetController.StartHidingEvent += InvokeHidingState;
        ExorcismItem.ExorcismChannelingEvent += InvokeExorcismState;
        ExorcismBar.FinishExorcismChannelingEvent += ResetPlayerState;
    }

    private void OnDisable()
    {
        AnkhItem.ChangeWorldGM -= InvokeChangeWorld;
        GhostManager.ChangeWorldGM -= InvokeChangeWorld;
        VictimController.VictimInteractionEvent -= InvokeDialogueState;
        DialogueManager.FinishDialogueEvent -= ResetPlayerState;
        ClosetController.StartHidingEvent -= InvokeHidingState;
        ExorcismItem.ExorcismChannelingEvent -= InvokeExorcismState;
        ExorcismBar.FinishExorcismChannelingEvent -= ResetPlayerState;
    }
    #endregion

    #region SendEvents
    public void SendHudPlayerEvent(Utils.PlayerHelper.States hudKey, bool condition)
    {
        HudPlayerEvent?.Invoke(hudKey, condition);
    }

    public void SendHudUiEvent(Utils.UiHelper.States hudKey, bool condition)
    {
        HudUiEvent?.Invoke(hudKey, condition);
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
        //Debug.Log("[MANAGER] Change World State");
    }

    public void InvokeDialogueState()
    {
        SendHudUiEvent(Utils.UiHelper.States.Dialogue, true);
        SendPlayerStateEvent(Utils.PlayerHelper.States.UI);
        //Debug.Log("[MANAGER] Change state to dialogue");
    }

    public void InvokeHidingState()
    {
        //SendHudPlayerEvent(Utils.PlayerHelper.States.Hiding, true);
        SendPlayerStateEvent(Utils.PlayerHelper.States.Hiding);
        //Debug.Log("[MANAGER] Change state to hiding");
    }

    public void InvokeExorcismState()
    {
        SendHudPlayerEvent(Utils.PlayerHelper.States.Exorcism, true);
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
