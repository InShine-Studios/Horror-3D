using System;
using UnityEngine.InputSystem;

public class DialogueState : PlayerState
{
    #region Events
    public static event Action NextDialogueHudEvent;
    #endregion

    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
    }
    #endregion

    #region InputHandler
    public override void NextDialogue(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            NextDialogueHudEvent?.Invoke();
        }
    }
    #endregion
}
