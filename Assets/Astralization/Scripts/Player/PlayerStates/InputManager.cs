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
    private PlayerState _currentPlayerState;
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        ChangeState<InitPlayerState>();
    }
    private void OnEnable()
    {
        GameManager.PlayerActionMapEvent += SetPlayerActionMap;
        HideInputHandler.StopHidingEvent += SetPlayerActionMap;
    }

    private void OnDisable()
    {
        GameManager.PlayerActionMapEvent -= SetPlayerActionMap;
        HideInputHandler.StopHidingEvent -= SetPlayerActionMap;
    }
    #endregion

    public void SetPlayerActionMap(string actionMap)
    {
        _playerInput.SwitchCurrentActionMap(actionMap);
        switch (actionMap) // RACE CONDITION
        {
            case "Default": ChangeState<DefaultPlayerState>(); break;
            case "Hiding": ChangeState<HidingState>(); break;
            case "Dialogue": ChangeState<DialogueState>(); break;
        }
        //Debug.Log("[PLAYER STATES] New Input Map: " + _playerInput.currentActionMap);
    }

    #region Input Handler
    private bool CanHandleInput ()
    {
        _currentPlayerState = (PlayerState)CurrentState;
        if (_currentPlayerState == null) return false;
        if (_inTransition) return false;
        return true;
    }

    public void HandleInputDefault(InputAction.CallbackContext ctx)
    {
        if (!CanHandleInput()) return;
        switch (ctx.action.name)
        {
            case "Movement": _currentPlayerState.OnMovementInput(ctx); break;
            case "MousePosition": _currentPlayerState.OnMousePosition(ctx); break;
            case "ChangeItem": _currentPlayerState.ScrollActiveItem(ctx); break;
            case "SprintStart": _currentPlayerState.SprintPressed(ctx); break;
            case "SprintEnd": _currentPlayerState.SprintReleased(ctx); break;
            case "Interact": _currentPlayerState.CheckInteractionInteractable(ctx); break;
            case "PickItem": _currentPlayerState.CheckInteractionItem(ctx); break;
            case "UseItem": _currentPlayerState.UseActiveItem(ctx); break;
            case "DiscardItem": _currentPlayerState.DiscardItemInput(ctx); break;
            case "SimulateGhostInteract": _currentPlayerState.CheckInteractionGhost(ctx); break;
        }  
    }

    public void HandleInputDialogue(InputAction.CallbackContext ctx)
    {
        if (!CanHandleInput()) return;
        switch (ctx.action.name)
        {
            case "NextDialogue": _currentPlayerState.NextDialogue(ctx); break;
        }
    }
    #endregion
}
