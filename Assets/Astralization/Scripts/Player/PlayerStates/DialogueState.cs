using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueState : PlayerState
{
    #region Variables
    private DialogueInputHandler _dialogueInputHandler;
    #endregion

    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        _dialogueInputHandler = GetComponentInChildren<DialogueInputHandler>();
    }
    #endregion

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
