using System;
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

    #region StateHandler
    public override void Enter()
    {
        base.Enter();
        Cursor.lockState = CursorLockMode.Locked;
    }
    #endregion

    #region Default Input Handler
    public virtual void OnMovementInput(InputAction.CallbackContext ctx)
    {
        Utils.LoggerHelper.PrintStateDefaultLog("Player","OnMovementInput");
    }
    public virtual void OnMouseDelta(InputAction.CallbackContext ctx)
    {
        Utils.LoggerHelper.PrintStateDefaultLog("Player","OnMousePosition");
    }
    public virtual void ScrollActiveItem(InputAction.CallbackContext ctx)
    {
        Utils.LoggerHelper.PrintStateDefaultLog("Player","ScrollActiveItem");
    }
    [Obsolete("Method is obsolete.", false)]
    public virtual void ChangeActiveItemQuickslot(InputAction.CallbackContext ctx)
    {
        Utils.LoggerHelper.PrintStateDefaultLog("Player", "ChangeActiveItemQuickslot");
    }
    public virtual void SprintPressed(InputAction.CallbackContext ctx)
    {
        Utils.LoggerHelper.PrintStateDefaultLog("Player","SprintPressed");
    }
    public virtual void SprintReleased(InputAction.CallbackContext ctx)
    {
        Utils.LoggerHelper.PrintStateDefaultLog("Player","SprintReleased");
    }
    public virtual void CheckInteractionInteractable(InputAction.CallbackContext ctx)
    {
        Utils.LoggerHelper.PrintStateDefaultLog("Player","CheckInteractionInteractable");
    }
    public virtual void CheckInteractionItem(InputAction.CallbackContext ctx)
    {
        Utils.LoggerHelper.PrintStateDefaultLog("Player","CheckInteractionItem");
    }
    public virtual void UseActiveItem(InputAction.CallbackContext ctx)
    {
        Utils.LoggerHelper.PrintStateDefaultLog("Player","UseActiveItem");
    }
    public virtual void DiscardItemInput(InputAction.CallbackContext ctx)
    {
        Utils.LoggerHelper.PrintStateDefaultLog("Player","DiscardItemInput");
    }
    public virtual void CheckInteractionGhost(InputAction.CallbackContext ctx)
    {
        Utils.LoggerHelper.PrintStateDefaultLog("Player","CheckInteractionGhost");
    }
    [Obsolete("Method is obsolete.", false)]
    public virtual void ToggleItemHudDisplay(InputAction.CallbackContext ctx)
    {
        Utils.LoggerHelper.PrintStateDefaultLog("Player", "ToggleItemHud");
    }
    public virtual void ToggleFlashlight(InputAction.CallbackContext ctx)
    {
        Utils.LoggerHelper.PrintStateDefaultLog("Player", "ToggleFlashlight");
    }

    public virtual void OpenMindMap(InputAction.CallbackContext ctx)
    {
        Utils.LoggerHelper.PrintStateDefaultLog("Player", "OpenMindMap");
    }
    #endregion

    #region Hiding Input Handler
    public virtual void UnhidePlayer(InputAction.CallbackContext ctx)
    {
        Utils.LoggerHelper.PrintStateDefaultLog("Player","UnhidePlayer");
    }
    #endregion

    #region Exorcism Input Handler
    public virtual void UseReleased(InputAction.CallbackContext ctx)
    {
        Utils.LoggerHelper.PrintStateDefaultLog("Player","UseReleasedExorcism");
    }
    #endregion

    #region MindMap Input Handler
    public virtual void CloseMindMap(InputAction.CallbackContext ctx)
    {
        Utils.LoggerHelper.PrintStateDefaultLog("Player", "CloseMindMap");
    }
    public virtual void ChangeCore(InputAction.CallbackContext ctx)
    {
        Utils.LoggerHelper.PrintStateDefaultLog("Player", "ChangeCore");
    }
    public virtual void ChangeClue(InputAction.CallbackContext ctx)
    {
        Utils.LoggerHelper.PrintStateDefaultLog("Player", "ChangeClue");
    }
    #endregion
}
