using System;
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
            InvokeStopHidingEvent();
            InvokeStopHidingHudEvent(false);
        }
    }
    #endregion

    #region InvokeEvents
    public void InvokeStopHidingEvent()
    {
        StopHidingEvent?.Invoke();
    }

    public void InvokeStopHidingHudEvent(bool isShown)
    {
        StopHidingHudEvent?.Invoke(isShown);
    }
    #endregion
}
