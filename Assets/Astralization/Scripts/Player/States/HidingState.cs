using System;
using UnityEngine.InputSystem;

public class HidingState : PlayerState
{
    #region Events
    public static event Action<bool> StopHidingHudEvent;
    public static event Action StopHidingEvent;
    #endregion

    protected override void Awake()
    {
        base.Awake();
    }

    #region InputHandler
    public override void UnhidePlayer(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            SendStopHidingEvent();
            SendStopHidingHudEvent(false);
        }
    }
    #endregion

    #region SendEvents
    public void SendStopHidingEvent()
    {
        StopHidingEvent?.Invoke();
    }

    public void SendStopHidingHudEvent(bool condition)
    {
        StopHidingHudEvent?.Invoke(condition);
    }
    #endregion
}
