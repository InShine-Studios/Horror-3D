using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerState : State
{
    #region Variables
    protected InputManager owner;
    #endregion

    #region MonoBehaviour
    protected virtual void Awake()
    {
        owner = GetComponent<InputManager>();
    }
    #endregion

    #region Default Input Handler
    public virtual void OnMovementInput(InputAction.CallbackContext ctx)
    {
        PrintDefaultLog("OnMovementInput");
    }
    public virtual void OnMousePosition(InputAction.CallbackContext ctx)
    {
        PrintDefaultLog("OnMousePosition");
    }
    public virtual void ScrollActiveItem(InputAction.CallbackContext ctx)
    {
        PrintDefaultLog("ScrollActiveItem");
    }
    public virtual void SprintPressed(InputAction.CallbackContext ctx)
    {
        PrintDefaultLog("SprintPressed");
    }
    public virtual void SprintReleased(InputAction.CallbackContext ctx)
    {
        PrintDefaultLog("SprintReleased");
    }
    public virtual void CheckInteractionInteractable(InputAction.CallbackContext ctx)
    {
        PrintDefaultLog("CheckInteractionInteractable");
    }
    public virtual void CheckInteractionItem(InputAction.CallbackContext ctx)
    {
        PrintDefaultLog("CheckInteractionItem");
    }
    public virtual void UseActiveItem(InputAction.CallbackContext ctx)
    {
        PrintDefaultLog("UseActiveItem");
    }
    public virtual void DiscardItemInput(InputAction.CallbackContext ctx)
    {
        PrintDefaultLog("DiscardItemInput");
    }
    public virtual void CheckInteractionGhost(InputAction.CallbackContext ctx)
    {
        PrintDefaultLog("CheckInteractionGhost");
    }
    #endregion

    #region Dialogue Input Handler
    public virtual void NextDialogue(InputAction.CallbackContext ctx)
    {
        PrintDefaultLog("NextDialogue");
    }
    #endregion

    #region Logger
    private void PrintDefaultLog(string methodName)
    {
        Debug.Log("[PLAYER STATE] Error: " + methodName +
            " is not implemented in this state");
    }
    #endregion
}
