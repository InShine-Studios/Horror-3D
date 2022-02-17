using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ExorcismInputHandler : MonoBehaviour
{
    public static event Action UseReleasedEvent;

    public void UseReleased(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            UseReleasedEvent?.Invoke();
        }
    }
}
