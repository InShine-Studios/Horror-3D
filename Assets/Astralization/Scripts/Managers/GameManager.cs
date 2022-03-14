using System;
using UnityEngine;

public interface IGameManager
{
    bool IsInAstralWorld();
    void InvokeChangeWorld();
}

public class GameManager : MonoBehaviour, IGameManager
{
    #region Event
    public static event Action<bool> ChangeWorldEvent;
    public static event Action<Utils.PlayerHelper.States> PlayerStateEvent;
    public static event Action<Utils.PlayerHelper.States, bool> HudPlayerEvent;
    public static event Action<Utils.UiHelper.States, bool> HudUiEvent;
    // TODO: to be implemented
    public static event Action PlayerAudioDiesEvent;
    #endregion

    #region Variables
    [Tooltip("Bool flag to check if the player is in Real World or Astral World")]
    [SerializeField]
    private bool _isInAstralWorld = false;
    #endregion

    #region SetGet
    public bool IsInAstralWorld() { return _isInAstralWorld; }
    #endregion

    #region MonoBehaviour
    private void OnEnable()
    {
        AnkhItem.ChangeWorldGM += InvokeChangeWorld;
        VictimController.VictimInteractionEvent += InvokeDialogueState;
        DialogueManager.FinishDialogueEvent += ResetPlayerState;
        ClosetsController.StartHidingEvent += InvokeHidingState;
        ExorcismItem.ExorcismChannelingEvent += InvokeExorcismState;
        ExorcismBar.FinishExorcismChannelingEvent += ResetPlayerState;
        HidingState.StopHidingEvent += ResetPlayerState;
    }

    private void OnDisable()
    {
        AnkhItem.ChangeWorldGM -= InvokeChangeWorld;
        VictimController.VictimInteractionEvent -= InvokeDialogueState;
        DialogueManager.FinishDialogueEvent -= ResetPlayerState;
        ClosetsController.StartHidingEvent -= InvokeHidingState;
        ExorcismItem.ExorcismChannelingEvent -= InvokeExorcismState;
        ExorcismBar.FinishExorcismChannelingEvent -= ResetPlayerState;
        HidingState.StopHidingEvent -= ResetPlayerState;
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
        _isInAstralWorld = !_isInAstralWorld;
        ChangeWorldEvent?.Invoke(_isInAstralWorld);
        //Debug.Log("[MANAGER] Changing world state to " + (_isInAstralWorld ? "Astral" : "Real"));
    }

    public void InvokeDialogueState()
    {
        SendHudUiEvent(Utils.UiHelper.States.Dialogue, true);
        SendPlayerStateEvent(Utils.PlayerHelper.States.UI);
        //Debug.Log("[MANAGER] Change state to dialogue");
    }

    public void InvokeHidingState()
    {
        SendHudPlayerEvent(Utils.PlayerHelper.States.Hiding, true);
        StartCoroutine(Utils.DelayerHelper.Delay(1.0f, () => SendPlayerStateEvent(Utils.PlayerHelper.States.Hiding)));
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
