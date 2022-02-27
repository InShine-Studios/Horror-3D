using UnityEngine;
using UnityEngine.InputSystem;

/*
 * Class to manage general input.
 * All undirect input flow should be managed by this class.
 */
public class InputManager : StateMachine
{
    #region Variables
    [Tooltip("The Player Input component")]
    [SerializeField]
    private PlayerInput _playerInput;
    private PlayerState _tempCurrentState;
    public enum States
    {
        Dialogue,
        Hiding,
        Exorcism,
        Default
    }
    #endregion

    private void Awake()
    {
        ChangeState<InitPlayerState>();
    }

    #region Enable Disable
    private void OnEnable()
    {
        GameManager.PlayerStateEvent += SetPlayerActionMap;
    }

    private void OnDisable()
    {
        GameManager.PlayerStateEvent -= SetPlayerActionMap;
    }

    #endregion

    public void SetPlayerActionMap(States actionMap)
    {
        _playerInput.SwitchCurrentActionMap(actionMap.ToString());
        switch (actionMap) // RACE CONDITION
        {
            case States.Default: ChangeState<DefaultPlayerState>(); break;
            case States.Hiding: ChangeState<HidingState>(); break;
            case States.Dialogue: ChangeState<DialogueState>(); break;
            case States.Exorcism: ChangeState<ExorcismState>(); break;
        }
        //Debug.Log("[INPUT MAP] New Map: " + _playerInput.currentActionMap);
    }

    #region Input Handler
    private bool CanHandleInput ()
    {
        _tempCurrentState = (PlayerState)CurrentState;
        if (_tempCurrentState == null) return false;
        if (_inTransition) return false;
        return true;
    }

    public void HandleInputDefault(InputAction.CallbackContext ctx)
    {
        if (!CanHandleInput()) return;
        switch (ctx.action.name)
        {
            case "Movement": _tempCurrentState.OnMovementInput(ctx); break;
            case "MousePosition": _tempCurrentState.OnMousePosition(ctx); break;
            case "ChangeItem": _tempCurrentState.ScrollActiveItem(ctx); break;
            case "SprintStart": _tempCurrentState.SprintPressed(ctx); break;
            case "SprintEnd": _tempCurrentState.SprintReleased(ctx); break;
            case "Interact": _tempCurrentState.CheckInteractionInteractable(ctx); break;
            case "PickItem": _tempCurrentState.CheckInteractionItem(ctx); break;
            case "UseItem": _tempCurrentState.UseActiveItem(ctx); break;
            case "DiscardItem": _tempCurrentState.DiscardItemInput(ctx); break;
            case "SimulateGhostInteract": _tempCurrentState.CheckInteractionGhost(ctx); break;
        }  
    }

    public void HandleInputDialogue(InputAction.CallbackContext ctx)
    {
        if (!CanHandleInput()) return;
        switch (ctx.action.name)
        {
            case "NextDialogue": _tempCurrentState.NextDialogue(ctx); break;
        }
    }

    public void HandleInputHiding(InputAction.CallbackContext ctx)
    {
        if (!CanHandleInput()) return;
        switch (ctx.action.name)
        {
            case "Interact": _tempCurrentState.UnhidePlayer(ctx); break;
        }
    }

    public void HandleInputExorcism(InputAction.CallbackContext ctx)
    {
        if (!CanHandleInput()) return;
        switch (ctx.action.name)
        {
            case "Channeling Stop": _tempCurrentState.UseReleased(ctx); break;
        }
    }
    #endregion
}
