using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueInvoker : MonoBehaviour
{
    #region Variable
    public static event Action DialogueInvoke;
    #endregion

    public void InvokeEvent()
    {
        DialogueInvoke?.Invoke();
    }

    public void EnterPressed(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            InvokeEvent();
        }
    }
}
