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

    private void Awake()
    {
        ChangeState<InitPlayerState>();
    }

    #region Enable Disable
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
        //Debug.Log("[INPUT MAP] New Map: " + _playerInput.currentActionMap);
    }

    #region Dialogue Input Handler

    public void HandleInputDefault(InputAction.CallbackContext ctx)
    {

        if (_inTransition) return;
        switch (ctx.action.name)
        {
            case "Movement": CurrentState.OnMovementInput(ctx); break;
            case "MousePosition": CurrentState.OnMousePosition(ctx); break;
            case "ChangeItem": CurrentState.ScrollActiveItem(ctx); break;
            case "SprintStart": CurrentState.SprintPressed(ctx); break;
            case "SprintEnd": CurrentState.SprintReleased(ctx); break;
            case "Interact": CurrentState.CheckInteractionInteractable(ctx); break;
            case "PickItem": CurrentState.CheckInteractionItem(ctx); break;
            case "UseItem": CurrentState.UseActiveItem(ctx); break;
            case "DiscardItem": CurrentState.DiscardItemInput(ctx); break;
            case "SimulateGhostInteract": CurrentState.CheckInteractionGhost(ctx); break;
        }  
    }

    public void HandleInputDialogue(InputAction.CallbackContext ctx)
    {
        if (_inTransition) return;
        switch (ctx.action.name)
        {
            case "NextDialogue": CurrentState.NextDialogue(ctx); break;
        }
    }
    #endregion
}
