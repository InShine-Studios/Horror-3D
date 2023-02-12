using System;
using UnityEngine;

public interface IGameManager
{
    void InvokeChangeWorld();
    void InvokeDialogueState();
    void InvokeExorcismState();
    void InvokeHidingState();
    void ResetPlayerState();
    void SendHudEvent(Utils.UiHelper.UiType hudKey, bool condition);
    void SendPlayerStateEvent(Utils.PlayerHelper.States actionMapKey);
}

public class GameManager : MonoBehaviour, IGameManager
{
    #region Event
    public static event Action ChangeWorldEvent;
    public static event Action<Utils.PlayerHelper.States> PlayerStateEvent;
    public static event Action<Utils.PlayerHelper.States> CameraStateEvent;
    public static event Action<Utils.UiHelper.UiType, bool> HudEvent;
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
        DummyTimeslotChanger.TimeslotIncrementEvent += InvokeTimeIncrement;
        InputManager.OpenMindMap += InvokeOpenMindMap;
        InputManager.CloseMindMap += ResetPlayerState;
        InputManager.ResetToDefault += ResetPlayerState;
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
        DummyTimeslotChanger.TimeslotIncrementEvent -= InvokeTimeIncrement;
        InputManager.OpenMindMap -= InvokeOpenMindMap;
        InputManager.CloseMindMap -= ResetPlayerState;
        InputManager.ResetToDefault -= ResetPlayerState;
    }
    #endregion

    #region SendEvents
    public void SendHudEvent(Utils.UiHelper.UiType hudKey, bool condition)
    {
        HudEvent?.Invoke(hudKey, condition);
    }

    public void SendPlayerStateEvent(Utils.PlayerHelper.States actionMapKey)
    {
        PlayerStateEvent?.Invoke(actionMapKey);
    }

    public void SendCameraStateEvent(Utils.PlayerHelper.States state)
    {
        CameraStateEvent?.Invoke(state);
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
        SendHudEvent(Utils.UiHelper.UiType.Dialogue, true);
        SendPlayerStateEvent(Utils.PlayerHelper.States.Dialogue);
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
        SendHudEvent(Utils.UiHelper.UiType.ExorcismBar, true);
        SendPlayerStateEvent(Utils.PlayerHelper.States.Exorcism);
        //Debug.Log("[MANAGER] Change state to exorcism");
    }

    public void InvokeMindMapState()
    {
        SendPlayerStateEvent(Utils.PlayerHelper.States.MindMap);
        SendCameraStateEvent(Utils.PlayerHelper.States.MindMap);
        //Debug.Log("[MANAGER] Reset player state to default");
    }

    public void ResetPlayerState()
    {
        SendPlayerStateEvent(Utils.PlayerHelper.States.Default);
        SendHudEvent(Utils.UiHelper.UiType.Default,true);
        SendCameraStateEvent(Utils.PlayerHelper.States.Default);
        //Debug.Log("[MANAGER] Reset player state to default");
    }

    public void InvokeTimeIncrement(int incrementCount)
    {
        TimeslotStateMachine.Instance.AdvanceTime(incrementCount);
    }

    public void InvokeOpenMindMap()
    {
        InvokeMindMapState();
        SendHudEvent(Utils.UiHelper.UiType.MindMap,true);
    }
    #endregion
}
