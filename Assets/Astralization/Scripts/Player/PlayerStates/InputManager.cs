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
        // for all UI related, add this condition
        if (actionMap.ToString().Equals("Dialogue"))
        {
            _playerInput.SwitchCurrentActionMap("UI");
        }
        else
        {
            _playerInput.SwitchCurrentActionMap(actionMap.ToString());
        }
        switch (actionMap) // RACE CONDITION
        {
            case Utils.PlayerHelper.States.Default: ChangeState<DefaultPlayerState>(); break;
            case Utils.PlayerHelper.States.Hiding: ChangeState<HidingState>(); break;
            case Utils.PlayerHelper.States.Dialogue: ChangeState<UiState>(); break;
            case Utils.PlayerHelper.States.Exorcism: ChangeState<ExorcismState>(); break;
        }
        //Debug.Log("[INPUT MAP] New Map: " + _playerInput.currentActionMap);
    }
    #endregion

    #region Input Handler
    private bool CanHandleInput()
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

    public void HandleInputUi(InputAction.CallbackContext ctx)
    {
        if (!CanHandleInput()) return;
        //switch (ctx.action.name)
        //{
        //    // TODO add here if there is UI component
        //}
    }

    public void HandleInputExorcism(InputAction.CallbackContext ctx)
    {
        if (!CanHandleInput()) return;
        switch (ctx.action.name)
        {
            case "ChannelingStop": _currentPlayerState.UseReleased(ctx); break;
        }
    }

    public void HandleInputHiding(InputAction.CallbackContext ctx)
    {
        if (!CanHandleInput()) return;
        switch (ctx.action.name)
        {
            case "Interact": _currentPlayerState.UnhidePlayer(ctx); break;
        }
    }
    #endregion
}
