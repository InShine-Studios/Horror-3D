using UnityEngine.InputSystem;

public class DialogueStaet : PlayerState
{
    #region Movement Variables
    private DialogueInputHandler _dialogueInputHandler;
    #endregion

    public override void Enter()
    {
        base.Enter();
        _dialogueInputHandler = GetComponent<DialogueInputHandler>();
    }

    protected override void HandleInput(InputAction input, InputAction.CallbackContext ctx)
    {
        switch (input.name)
        {
            case "NextDialogue": _dialogueInputHandler.NextDialogue(ctx); break;
        }
    }
}
