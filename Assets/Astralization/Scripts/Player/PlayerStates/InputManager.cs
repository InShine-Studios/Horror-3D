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
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        ChangeState<PlayerInitState>();
    }

    private void OnEnable()
    {
        GameManager.PlayerStateEvent += SetPlayerActionMap;
    }

    private void OnDisable()
    {
        GameManager.PlayerStateEvent -= SetPlayerActionMap;
    }
    #endregion

    #region SetGet
    public void SetPlayerActionMap(Utils.PlayerHelper.States actionMap)
    {
        _playerInput.SwitchCurrentActionMap(actionMap.ToString());
        switch (actionMap) // RACE CONDITION
        {
            case Utils.PlayerHelper.States.Default: ChangeState<PlayerDefaultState>(); break;
            case Utils.PlayerHelper.States.Hiding: ChangeState<PlayerHidingState>(); break;
            case Utils.PlayerHelper.States.UI: ChangeState<PlayerUiState>(); break;
            case Utils.PlayerHelper.States.Exorcism: ChangeState<PlayerExorcismState>(); break;
        }
        //Debug.Log("[INPUT MAP] New Map: " + _playerInput.currentActionMap);
    }
    #endregion

    #region Input Handler
    private bool CanHandleInput()
    {
        if (CurrentState == null) return false;
        if (_inTransition) return false;
        return true;
    }

    public void HandleInputDefault(InputAction.CallbackContext ctx)
    {
        PlayerState currentPlayerState = (PlayerState)CurrentState;
        if (!CanHandleInput()) return;
        switch (ctx.action.name)
        {
            case "Movement": currentPlayerState.OnMovementInput(ctx); break;
            case "MouseDelta": currentPlayerState.OnMouseDelta(ctx); break;
            case "ChangeItem": currentPlayerState.ScrollActiveItem(ctx); break;
            case "SprintStart": currentPlayerState.SprintPressed(ctx); break;
            case "SprintEnd": currentPlayerState.SprintReleased(ctx); break;
            case "Interact": currentPlayerState.CheckInteractionInteractable(ctx); break;
            case "PickItem": currentPlayerState.CheckInteractionItem(ctx); break;
            case "UseItem": currentPlayerState.UseActiveItem(ctx); break;
            case "DiscardItem": currentPlayerState.DiscardItemInput(ctx); break;
            case "SimulateGhostInteract": currentPlayerState.CheckInteractionGhost(ctx); break;
            case "InventoryQuickslot": currentPlayerState.ChangeActiveItemQuickslot(ctx); break;
            case "ToggleItemHudDisplay": currentPlayerState.ToggleItemHudDisplay(ctx); break;
            case "ToggleFlashlight": currentPlayerState.ToggleFlashlight(ctx); break;
        }  
    }

    public void HandleInputExorcism(InputAction.CallbackContext ctx)
    {
        PlayerState currentPlayerState = (PlayerState)CurrentState;
        if (!CanHandleInput()) return;
        switch (ctx.action.name)
        {
            case "ChannelingStop": currentPlayerState.UseReleased(ctx); break;
        }
    }

    public void HandleInputHiding(InputAction.CallbackContext ctx)
    {
        PlayerState currentPlayerState = (PlayerState)CurrentState;
        if (!CanHandleInput()) return;
        switch (ctx.action.name)
        {
            case "Interact": currentPlayerState.UnhidePlayer(ctx); break;
        }
    }
    #endregion
}
