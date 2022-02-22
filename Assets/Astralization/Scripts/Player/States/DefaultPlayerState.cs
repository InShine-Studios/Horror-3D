using UnityEngine;
using UnityEngine.InputSystem;

public class DefaultPlayerState : PlayerState
{
    #region Handler Variables
    [Header("Movement and Rotation")]
    private PlayerMovement _playerMovement;
    private PlayerRotation _playerRotation;

    [Header("Inventory and Detector")]
    private Inventory _inventory;
    private InteractableDetector _interactableDetector;
    private ItemDetector _itemDetector;
    private GhostSimulationInteractableDetector _ghostSimulationInteractableDetector;
    #endregion

    protected override void Awake()
    {
        base.Awake();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerRotation = GetComponentInChildren<PlayerRotation>();
        _inventory = GetComponentInChildren<Inventory>();
        _interactableDetector = GetComponentInChildren<InteractableDetector>();
        _itemDetector = GetComponentInChildren<ItemDetector>();
        _ghostSimulationInteractableDetector = GetComponentInChildren<GhostSimulationInteractableDetector>();
    }

    #region Input Handler
    #region Movement
    public override void OnMovementInput(InputAction.CallbackContext ctx)
    {
        _playerMovement.OnMovementInput(ctx.ReadValue<Vector2>());
    }

    public override void OnMousePosition(InputAction.CallbackContext ctx)
    {
        _playerRotation.OnMousePosition(ctx.ReadValue<Vector2>());
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
    #endregion

    #region Inventory
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

    public override void ScrollActiveItem(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _inventory.ScrollActiveItem(ctx.ReadValue<Vector2>());
        }
    }
    #endregion

    #region Detector
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

    public override void CheckInteractionGhost(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _ghostSimulationInteractableDetector.CheckInteraction();
        }
    }
    #endregion
    #endregion
}
