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
    // For further player state storage
    private enum _playerState
    {
        Dialogue,
        Hiding,
        Player
    }
    #endregion

    #region Event
    public static event Action<bool> ChangeWorldEvent;
    public static event Action<string> PlayerActionMapEvent;
    public static event Action<bool> ShowDialogueHudEvent;
    public static event Action<bool> StartHidingHudEvent;
    // TODO: to be implemented
    public static event Action PlayerAudioDiesEvent;
    #endregion

    #region Enable - Disable
    private void OnEnable()
    {
        AnkhItem.ChangeWorldGM += InvokeChangeWorld;
        NpcController.NpcInteractionEvent += InvokePlayerState;
        DialogueManager.FinishDialogueEvent += InvokePlayerState;
        ClosetsController.StartHidingEvent += InvokePlayerState;
    }

    private void OnDisable()
    {
        AnkhItem.ChangeWorldGM -= InvokeChangeWorld;
        NpcController.NpcInteractionEvent -= InvokePlayerState;
        DialogueManager.FinishDialogueEvent -= InvokePlayerState;
        ClosetsController.StartHidingEvent -= InvokePlayerState;
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

    public void InvokePlayerState(String state)
    {
        //Debug.Log("[INVOKE PLAYER STATE] Player state: " + state);
        if (state.Equals(_playerState.Dialogue.ToString()))
        {
            ShowDialogueHudEvent?.Invoke(true);
            PlayerActionMapEvent?.Invoke(_playerState.Dialogue.ToString());
        }
        else if (state.Equals(_playerState.Hiding.ToString()))
        {
            StartHidingHudEvent?.Invoke(true);
            PlayerActionMapEvent?.Invoke(_playerState.Hiding.ToString());
        }
        else if (state.Equals(_playerState.Player.ToString()))
        {
            PlayerActionMapEvent?.Invoke(_playerState.Player.ToString());
        }
    }
    #endregion
}
