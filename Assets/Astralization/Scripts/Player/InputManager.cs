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
    private PlayerState tempCurrentState;
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
        DialogueManager.DialogueChoiceSetInputEvent += SetEnablePlayerInput;
    }

    private void OnDisable()
    {
        GameManager.PlayerActionMapEvent -= SetPlayerActionMap;
        HideInputHandler.StopHidingEvent -= SetPlayerActionMap;
        DialogueManager.DialogueChoiceSetInputEvent -= SetEnablePlayerInput;
    }

    #endregion

    #region SetterGetter
    public void SetEnablePlayerInput(bool isEnable)
    {
        //Debug.Log("[INPUT MANAGER] SetEnablePlayerInput " + isEnable);
        if (isEnable)
        {
            _playerInput.SwitchCurrentActionMap("Dialogue");
            _playerInput.currentActionMap.Enable();
        }
        else
        {
            _playerInput.SwitchCurrentActionMap("Dialogue Choice");
            //_playerInput.currentActionMap.Disable();
        }
    }

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
    #endregion

    #region Input Handler
    private bool CanHandleInput ()
    {
        tempCurrentState = (PlayerState)CurrentState;
        if (tempCurrentState == null) return false;
        if (_inTransition) return false;
        return true;
    }

    public void HandleInputDefault(InputAction.CallbackContext ctx)
    {
        if (!CanHandleInput()) return;
        switch (ctx.action.name)
        {
            case "Movement": tempCurrentState.OnMovementInput(ctx); break;
            case "MousePosition": tempCurrentState.OnMousePosition(ctx); break;
            case "ChangeItem": tempCurrentState.ScrollActiveItem(ctx); break;
            case "SprintStart": tempCurrentState.SprintPressed(ctx); break;
            case "SprintEnd": tempCurrentState.SprintReleased(ctx); break;
            case "Interact": tempCurrentState.CheckInteractionInteractable(ctx); break;
            case "PickItem": tempCurrentState.CheckInteractionItem(ctx); break;
            case "UseItem": tempCurrentState.UseActiveItem(ctx); break;
            case "DiscardItem": tempCurrentState.DiscardItemInput(ctx); break;
            case "SimulateGhostInteract": tempCurrentState.CheckInteractionGhost(ctx); break;
        }  
    }

    public void HandleInputDialogue(InputAction.CallbackContext ctx)
    {
        if (!CanHandleInput()) return;
        switch (ctx.action.name)
        {
            case "NextDialogue": tempCurrentState.NextDialogue(ctx); break;
        }
    }
    #endregion
}
