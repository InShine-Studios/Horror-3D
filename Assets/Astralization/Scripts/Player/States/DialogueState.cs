using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueState : PlayerState
{
    #region Handler Variables
    private DialogueInputHandler _dialogueInputHandler;
    #endregion

    protected override void Awake()
    {
        base.Awake();
        _dialogueInputHandler = GetComponentInChildren<DialogueInputHandler>();
    }

    #region Input Handler
    public override void NextDialogue(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _dialogueInputHandler.NextDialogue();
        }
    }
    #endregion
}
