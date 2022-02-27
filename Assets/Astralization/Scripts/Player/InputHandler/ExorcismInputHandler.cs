using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 * Class to accept player input when releasing button during exorcism
 */
public class ExorcismInputHandler : MonoBehaviour
{
    #region Events
    public static event Action UseReleasedEvent;
    #endregion

    #region Handler
    public void UseReleased(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            UseReleasedEvent?.Invoke();
        }
    }
    #endregion
}
