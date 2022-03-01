using UnityEngine;
using UnityEngine.InputSystem;

public class DefaultPlayerState : PlayerState
{
    #region Variables
    [Header("Movement and Rotation")]
    private PlayerMovement _playerMovement;
    private PlayerRotation _playerRotation;

    [Header("Inventory and Detector")]
    private Inventory _inventory;
    private InteractableDetector _interactableDetector;
    private ItemDetector _itemDetector;
    private GhostSimulationInteractableDetector _ghostSimulationInteractableDetector;
    #endregion

    #region MonoBehaviour
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
    #endregion

    #region Movement Input Handler
    public override void OnMovementInput(InputAction.CallbackContext ctx)
    {
        _playerMovement.GenerateMoveVector(ctx.ReadValue<Vector2>());
    }

    public override void OnMousePosition(InputAction.CallbackContext ctx)
    {
        _playerRotation.SetMousePosition(ctx.ReadValue<Vector2>());
    }

    public override void SprintPressed(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _playerMovement.SetSprinting(true);
        }
    }

    public override void SprintReleased(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _playerMovement.SetSprinting(false);
        }
    }
    #endregion

    #region Inventory Input Handler
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

    #region Detector Input Handler
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
}
