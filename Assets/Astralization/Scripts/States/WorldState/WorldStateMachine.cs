using Astralization.Enemy;
using Astralization.Managers;
using Astralization.SPI;
using Astralization.States.TimeslotStates;
using UnityEngine;

namespace Astralization.States.WorldStates
{
    public class WorldStateMachine : StateMachine
    {
        #region Variables
        [Tooltip("Bool flag to check if the player is in Real World or Astral World")]
        private bool _isInAstralWorld = false;

        private GhostManager _ghostManager;

        private static WorldStateMachine _instance;
        public static WorldStateMachine Instance { get { return _instance; } }
        #endregion

        #region SetGet
        public bool IsKillPhase()
        {
            if (_ghostManager == null) return false;
            return _ghostManager.IsKillPhase();
        }
        #endregion

        #region MonoBehaviour
        private void Awake()
        {
            _ghostManager = GameObject.Find("Ghost")?.GetComponent<GhostManager>(); //TODO: Refactor this
            ChangeState<WorldInitState>();
            _instance = this;
        }

        private void OnEnable()
        {
            GameManager.ChangeWorldEvent += ChangeState;
        }

        private void OnDisable()
        {
            GameManager.ChangeWorldEvent -= ChangeState;
        }
        #endregion

        #region WorldHandler
        protected virtual void ChangeState()
        {
            _isInAstralWorld = !_isInAstralWorld;
            if (_isInAstralWorld)
            {
                ChangeState<WorldAstralState>();
                //Debug.Log("[WORLD STATE] Changing world state to Astral");
            }
            else
            {
                ChangeState<WorldRealState>();
                //Debug.Log("[WORLD STATE] Changing world state to Real");
            }
        }
        #endregion
    }
}