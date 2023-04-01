using Astralization.Enemy;
using Astralization.Interactables.Closet;
using Astralization.Interactables.TimeslotChanger;
using Astralization.Interactables.Victim;
using Astralization.Items;
using Astralization.Items.EvidenceItems.ExorcismItems;
using Astralization.Player.PlayerStates;
using Astralization.States.TimeslotStates;
using Astralization.UI;
using Astralization.Utils.Helper;
using System;
using UnityEngine;

namespace Astralization.Managers
{
    public interface IGameManager
    {
        void InvokeChangeWorld();
        void InvokeDialogueState();
        void InvokeExorcismState();
        void InvokeHidingState();
        void ResetPlayerState();
        void SendHudEvent(UiHelper.UiType hudKey, bool condition);
        void SendPlayerStateEvent(PlayerHelper.States actionMapKey);
    }

    public class GameManager : MonoBehaviour, IGameManager
    {
        #region Event
        public static event Action ChangeWorldEvent;
        public static event Action<PlayerHelper.States> PlayerStateEvent;
        public static event Action<PlayerHelper.States> CameraStateEvent;
        public static event Action<UiHelper.UiType, bool> HudEvent;
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
        public void SendHudEvent(UiHelper.UiType hudKey, bool condition)
        {
            HudEvent?.Invoke(hudKey, condition);
        }

        public void SendPlayerStateEvent(PlayerHelper.States actionMapKey)
        {
            PlayerStateEvent?.Invoke(actionMapKey);
        }

        public void SendCameraStateEvent(PlayerHelper.States state)
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
            SendHudEvent(UiHelper.UiType.Dialogue, true);
            SendPlayerStateEvent(PlayerHelper.States.Dialogue);
            //Debug.Log("[MANAGER] Change state to dialogue");
        }

        public void InvokeHidingState()
        {
            //SendHudPlayerEvent(Utils.PlayerHelper.States.Hiding, true);
            SendPlayerStateEvent(PlayerHelper.States.Hiding);
            //Debug.Log("[MANAGER] Change state to hiding");
        }

        public void InvokeExorcismState()
        {
            SendHudEvent(UiHelper.UiType.ExorcismBar, true);
            SendPlayerStateEvent(PlayerHelper.States.Exorcism);
            //Debug.Log("[MANAGER] Change state to exorcism");
        }

        public void InvokeMindMapState()
        {
            SendPlayerStateEvent(PlayerHelper.States.MindMap);
            SendCameraStateEvent(PlayerHelper.States.MindMap);
            //Debug.Log("[MANAGER] Reset player state to default");
        }

        public void ResetPlayerState()
        {
            SendPlayerStateEvent(PlayerHelper.States.Default);
            SendHudEvent(UiHelper.UiType.Default, true);
            SendCameraStateEvent(PlayerHelper.States.Default);
            //Debug.Log("[MANAGER] Reset player state to default");
        }

        public void InvokeTimeIncrement(int incrementCount)
        {
            TimeslotStateMachine.Instance.AdvanceTime(incrementCount);
        }

        public void InvokeOpenMindMap()
        {
            InvokeMindMapState();
            SendHudEvent(UiHelper.UiType.MindMap, true);
        }
        #endregion
    }
}