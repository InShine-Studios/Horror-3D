using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueInvoker : MonoBehaviour
{
    #region Variable
    public static event Action StartDialogue;
    #endregion

    public void InvokeEvent()
    {
        StartDialogue?.Invoke();
    }

    public void EnterPressed(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            InvokeEvent();
        }
    }
}
