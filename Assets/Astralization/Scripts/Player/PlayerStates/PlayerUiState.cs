using UnityEngine;
using Cinemachine;
using Astralization.Utils.Helper;

namespace Astralization.Player.PlayerStates
{

    /*
     * Class to manage player when in UI state (dialogue, journal, etc).
     * All logic-related players in UI state will be handled here.
     */
    public abstract class PlayerUiState : PlayerState
    {
        #region Variables
        protected CinemachineBrain _cinemachineBrain;
        protected bool _confineCursor = false;
        #endregion

        #region MonoBehaviour
        protected override void Awake()
        {
            base.Awake();
            _cinemachineBrain = GetComponent<PlayerMovement>().GetMainCamera().GetComponent<CinemachineBrain>();
        }
        #endregion

        #region StateHandler
        public override void Enter()
        {
            base.Enter();
            if (_confineCursor) Cursor.lockState = CursorLockMode.Confined;
            StartCoroutine(DelayerHelper.Delay(0.25f, () => _cinemachineBrain.enabled = false));
        }

        public override void Exit()
        {
            base.Exit();
            Cursor.lockState = CursorLockMode.Locked;
            _cinemachineBrain.enabled = true;
        }
        #endregion
    }
}