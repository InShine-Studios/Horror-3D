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
    public static event Action StopHidingEvent;
    #endregion

    public void UnhidePlayer()
    {
        StopHidingEvent?.Invoke();
        StopHidingHudEvent?.Invoke(false);
    }
}
