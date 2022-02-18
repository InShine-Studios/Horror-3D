using UnityEngine;
using UnityEngine.InputSystem;

public class DefaultPlayerState : PlayerState
{
    #region Movement Variables
    private PlayerMovement _playerMovement;
    private InteractableDetector _interactableDetector;
    private ItemDetector _itemDetector;
    private Inventory _inventory;
    private PlayerRotation _playerRotation;
    private GhostSimulationInteractableDetector _ghostSimulationInteractableDetector;
    #endregion

    protected override void Awake()
    {
        base.Awake();
        _playerMovement = GetComponent<PlayerMovement>();
        _interactableDetector = GetComponentInChildren<InteractableDetector>();
        _itemDetector = GetComponentInChildren<ItemDetector>();
        _inventory = GetComponentInChildren<Inventory>();
        _playerRotation = GetComponentInChildren<PlayerRotation>();
        _ghostSimulationInteractableDetector = GetComponentInChildren<GhostSimulationInteractableDetector>();
    }

    #region Input Handler
    public override void OnMovementInput(InputAction.CallbackContext ctx)
    {
        _playerMovement.OnMovementInput(ctx.ReadValue<Vector2>());
    }

    public override void OnMousePosition(InputAction.CallbackContext ctx)
    {
        _playerRotation.OnMousePosition(ctx.ReadValue<Vector2>());
    }

    public override void ScrollActiveItem(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _inventory.ScrollActiveItem(ctx.ReadValue<Vector2>());
        }
    }

    public override void SprintPressed(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _playerMovement.SprintPressed();
        }
    }

    public override void SprintReleased(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _playerMovement.SprintReleased();
        }
    }

    public override void CheckInteractionInteractable(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _interactableDetector.CheckInteraction();
        }
    }

    public override void CheckInteractionItem(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _itemDetector.CheckInteraction();
        }
    }

    public override void UseActiveItem(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _inventory.UseActiveItem();
        }
    }

    public override void DiscardItemInput(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _inventory.DiscardItemInput();
        }
    }

    public override void CheckInteractionGhost(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _ghostSimulationInteractableDetector.CheckInteraction();
        }
    }
    #endregion
}
