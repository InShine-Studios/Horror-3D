using System;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 * Class to accept player input and continue with the dialogue
 */
public class PlayerDialogueInvoker : MonoBehaviour
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
