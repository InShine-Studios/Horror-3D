using UnityEngine.InputSystem;

public class ExorcismState : PlayerState
{
    #region Handler Variables
    private ExorcismInputHandler _exorcismInputHandler;
    #endregion

    protected override void Awake()
    {
        base.Awake();
        _exorcismInputHandler = GetComponentInChildren<ExorcismInputHandler>();
    }

    #region Input Handler
    public override void UseReleased(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _exorcismInputHandler.UseReleased();
        }
    }
    #endregion
}
