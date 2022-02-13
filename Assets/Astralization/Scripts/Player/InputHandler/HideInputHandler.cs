using System;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 * Class to accept player input and continue with the dialogue
 */
public class HideInputHandler : MonoBehaviour
{
    #region Variable
    public static event Action<bool> StopHidingHudEvent;
    public static event Action<string> StopHidingEvent;

    private string _playerActionMap = "Player";
    #endregion

    public void UnHidePlayer(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            StopHidingEvent?.Invoke(_playerActionMap);
            StopHidingHudEvent?.Invoke(false);
        }
    }
}
