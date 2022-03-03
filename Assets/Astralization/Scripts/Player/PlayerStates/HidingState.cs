using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class HidingState : PlayerState
{
    #region Events
    public static event Action<bool> StopHidingHudEvent;
    public static event Action StopHidingEvent;
    #endregion

    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
    }
    #endregion

    #region InputHandler
    public override void UnhidePlayer(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            StopHidingEvent?.Invoke();
            StopHidingHudEvent?.Invoke(false);
        }
    }
    #endregion
}
