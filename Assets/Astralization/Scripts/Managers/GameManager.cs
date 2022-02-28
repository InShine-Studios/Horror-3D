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
    public static event Action<Utils.PlayerStatesEnum.States> PlayerStateEvent;
    public static event Action<Utils.PlayerStatesEnum.States, bool> HudEvent;
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
    public void SendHudEvent(Utils.PlayerStatesEnum.States hudKey, bool conditions)
    {
        HudEvent?.Invoke(hudKey, conditions);
    }

    public void SendPlayerStateEvent(Utils.PlayerStatesEnum.States actionMapKey)
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
        SendHudEvent(Utils.PlayerStatesEnum.States.Dialogue, true);
        SendPlayerStateEvent(Utils.PlayerStatesEnum.States.Dialogue);
    }

    public void InvokeHidingState()
    {
        SendHudEvent(Utils.PlayerStatesEnum.States.Hiding, true);
        SendPlayerStateEvent(Utils.PlayerStatesEnum.States.Hiding);
    }

    public void InvokeExorcismState()
    {
        SendHudEvent(Utils.PlayerStatesEnum.States.Exorcism, true);
        SendPlayerStateEvent(Utils.PlayerStatesEnum.States.Exorcism);
    }

    public void ResetPlayerState()
    {
        //Debug.Log("[INVOKE PLAYER STATE] Player state");
        SendPlayerStateEvent(Utils.PlayerStatesEnum.States.Default);
    }
    #endregion
}
