using System;
using UnityEngine.InputSystem;

namespace Astralization.Player.PlayerStates
{
    public class PlayerExorcismState : PlayerState
    {
        #region Events
        public static event Action StopExorcismEvent;
        #endregion

        protected override void Awake()
        {
            base.Awake();
        }

        #region InputHandler
        public override void UseReleased(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                StopExorcismEvent?.Invoke();
            }
        }
        #endregion
    }
}