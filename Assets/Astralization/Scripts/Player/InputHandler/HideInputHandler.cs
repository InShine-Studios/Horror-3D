using System;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 * Class to accept player input and continue with the dialogue
 */
public class HideInputHandler : MonoBehaviour
{
    private string _playerActionMap = "Player";

    #region Events
    public static event Action<bool> StopHidingHudEvent;
    public static event Action<string> StopHidingEvent;
    #endregion

    public void UnhidePlayer(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            StopHidingEvent?.Invoke(_playerActionMap);
            StopHidingHudEvent?.Invoke(false);
        }
    }
}
