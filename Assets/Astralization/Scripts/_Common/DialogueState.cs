using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueState : PlayerState
{
    #region Movement Variables
    private DialogueInputHandler _dialogueInputHandler;
    #endregion

    public override void Enter()
    {
        base.Enter();
        _dialogueInputHandler = GetComponentInChildren<DialogueInputHandler>();
    }

    public override void HandleInput(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            switch (ctx.action.name)
            {
                case "NextDialogue": _dialogueInputHandler.NextDialogue(); break;
            }
        }
    }
}
