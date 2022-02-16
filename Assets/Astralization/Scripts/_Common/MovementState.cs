using UnityEngine.InputSystem;

public class MovementState : PlayerState
{
    #region Movement Variables
    private PlayerMovement _playerMovement;
    private InteractableDetector _interactableDetector;
    private ItemDetector _itemDetector;
    private Inventory _inventory;
    #endregion

    public override void Enter()
    {
        base.Enter();
        _playerMovement = GetComponent<PlayerMovement>();
        _interactableDetector = GetComponent<InteractableDetector>();
        _itemDetector = GetComponent<ItemDetector>();
        _inventory = GetComponent<Inventory>();
    }

    protected override void HandleInput(InputAction input, InputAction.CallbackContext ctx)
    {
        switch (input.name)
        {
            case "Movement": _playerMovement.OnMovementInput(ctx); break;
            case "SprintStart": _playerMovement.SprintPressed(ctx); break;
            case "SprintEnd": _playerMovement.SprintReleased(ctx); break;
            case "Interact": _interactableDetector.CheckInteraction(ctx); break;
            case "PickItem": _itemDetector.CheckInteraction(ctx); break;
            case "ChangeItem": _inventory.ScrollActiveItem(ctx); break;
            case "UseItem": _inventory.UseActiveItem(ctx); break;
            case "DiscardItem": _inventory.DiscardItemInput(ctx); break;
        }
    }
}
