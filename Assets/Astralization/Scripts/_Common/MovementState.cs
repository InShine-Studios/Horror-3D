using UnityEngine;
using UnityEngine.InputSystem;

public class MovementState : PlayerState
{
    #region Movement Variables
    private PlayerMovement _playerMovement;
    private InteractableDetector _interactableDetector;
    private ItemDetector _itemDetector;
    private Inventory _inventory;
    private PlayerRotation _playerRotation;
    private GhostSimulationInteractableDetector _ghostSimulationInteractableDetector;
    #endregion

    public override void Enter()
    {
        base.Enter();
        _playerMovement = GetComponent<PlayerMovement>();
        _interactableDetector = GetComponentInChildren<InteractableDetector>();
        _itemDetector = GetComponentInChildren<ItemDetector>();
        _inventory = GetComponentInChildren<Inventory>();
        _playerRotation = GetComponentInChildren<PlayerRotation>();
        _ghostSimulationInteractableDetector = GetComponentInChildren<GhostSimulationInteractableDetector>();
    }

    public override void HandleInput(InputAction.CallbackContext ctx)
    {
        if (ctx.action.name.Equals("Movement"))
        {
            _playerMovement.OnMovementInput(ctx);
        }
        else if (ctx.action.name.Equals("MousePosition"))
        {
            _playerRotation.OnMousePosition(ctx);
        }
        else if (ctx.action.name.Equals("ChangeItem"))
        {
            _inventory.ScrollActiveItem(ctx);
        }
        else if (ctx.performed)
        {
            switch (ctx.action.name)
            {
                case "SprintStart": _playerMovement.SprintPressed(); break;
                case "SprintEnd": _playerMovement.SprintReleased(); break;
                case "Interact": _interactableDetector.CheckInteraction(); break;
                case "PickItem": _itemDetector.CheckInteraction(); break;
                case "UseItem": _inventory.UseActiveItem(); break;
                case "DiscardItem": _inventory.DiscardItemInput(); break;
                case "SimulateGhostInteract": _ghostSimulationInteractableDetector.CheckInteraction(); break;
            }
        }
    }
}
