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
    private enum _playerState
    {
        Dialogue,
        Hiding,
        Exorcism,
        Default
    }
    #endregion

    #region Event
    public static event Action<bool> ChangeWorldEvent;
    public static event Action<string> PlayerStateEvent;
    public static event Action<string, bool> HudEvent;
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
    }

    private void OnDisable()
    {
        AnkhItem.ChangeWorldGM -= InvokeChangeWorld;
        NpcController.NpcInteractionEvent -= InvokeDialogueState;
        DialogueManager.FinishDialogueEvent -= ResetPlayerState;
        ClosetsController.StartHidingEvent -= InvokeHidingState;
        ExorcismItem.ExorcismChannelingEvent -= InvokeExorcismState;
        ExorcismBar.FinishExorcismChannelingEvent -= ResetPlayerState;
    }
    #endregion

    #region Setter Getter
    public bool IsInAstralWorld() { return _isInAstralWorld; }
    #endregion

    #region SendEvents
    public void SendHudEvent(string hudKey, bool conditions)
    {
        HudEvent?.Invoke(hudKey, conditions);
    }

    public void SendPlayerStateEvent(string actionMapKey)
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
        SendHudEvent(_playerState.Dialogue.ToString(), true);
        SendPlayerStateEvent(_playerState.Dialogue.ToString());
    }

    public void InvokeHidingState()
    {
        SendHudEvent(_playerState.Hiding.ToString(), true);
        SendPlayerStateEvent(_playerState.Hiding.ToString());
    }

    public void InvokeExorcismState()
    {
        SendHudEvent(_playerState.Exorcism.ToString(), true);
        SendPlayerStateEvent(_playerState.Exorcism.ToString());
    }

    public void ResetPlayerState()
    {
        //Debug.Log("[INVOKE PLAYER STATE] Player state: " + state);
        SendPlayerStateEvent(_playerState.Default.ToString());
    }
    #endregion
}
