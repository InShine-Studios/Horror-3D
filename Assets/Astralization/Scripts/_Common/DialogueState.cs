using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueState : PlayerState
{
    #region Movement Variables
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

    public override void CheckInteractionInteractable(InputAction.CallbackContext ctx)
    {
        Debug.Log("aaaa");
    }
    #endregion
}
