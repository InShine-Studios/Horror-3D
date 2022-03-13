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
    public static event Action<Utils.PlayerHelper.States, bool> HudEvent;
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
        _isInAstralWorld = !_isInAstralWorld;
        ChangeWorldEvent?.Invoke(_isInAstralWorld);
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
        Action SendPlayerStateEventAction = () => SendPlayerStateEvent(Utils.PlayerHelper.States.Hiding);
        StartCoroutine(Utils.DelayerHelper.Delay(1.0f, SendPlayerStateEventAction));
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
