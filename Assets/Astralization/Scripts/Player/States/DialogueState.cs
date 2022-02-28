using System;
using UnityEngine.InputSystem;

public class DialogueState : PlayerState
{
    #region Events
    public static event Action NextDialogueHudEvent;
    #endregion

    protected override void Awake()
    {
        base.Awake();
    }

    #region InputHandler
    public override void NextDialogue(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            SendNextDialogueHudEvent();
        }
    }
    #endregion

    #region SendEvents
    public void SendNextDialogueHudEvent()
    {
        NextDialogueHudEvent?.Invoke();
    }
    #endregion
}
