using UnityEngine.InputSystem;

public class HidingState : PlayerState
{
    #region Handler Variables
    private HideInputHandler _hideInputHandler;
    #endregion

    protected override void Awake()
    {
        base.Awake();
        _hideInputHandler = GetComponentInChildren<HideInputHandler>();
    }

    #region Input Handler
    public override void UnhidePlayer(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _hideInputHandler.UnhidePlayer();
        }
    }
    #endregion
}
