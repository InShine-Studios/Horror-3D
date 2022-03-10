using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class HidingState : PlayerState
{
    #region Events
    public static event Action<bool> StopHidingHudEvent;
    public static event Action StopHidingEvent;
    #endregion

    #region Variables
    [Tooltip("Closets interactable that been interacted by player")]
    private Interactable _closets;
    [Tooltip("PlayerMovement object that gonna be toogled")]
    private PlayerMovement _playerMovement;
    [Tooltip("Player previous position")]
    private Vector3 _prevPosition;
    [Tooltip("False if player change position")]
    private bool _playerMovementConditions = false;
    #endregion

    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        _playerMovement = GetComponent<PlayerMovement>();
    }
    #endregion

    #region Handler
    public override void Enter()
    {
        base.Enter();
        PlayerMovementChangeState();
        _closets = GetComponentInChildren<InteractableDetector>().GetClosest();
        _prevPosition = this.transform.position;
        Vector3 closetsPosition = _closets.transform.parent.position;
        Vector3 calOffset = _closets.transform.parent.rotation * new Vector3(0.97f,1.1f,-0.47f);
        this.transform.position = closetsPosition + calOffset;
    }

    public override void Exit()
    {
        base.Exit();
        this.transform.position = _prevPosition;
        _closets = null;
        Invoke("PlayerMovementChangeState", 1.0f);
    }

    private void PlayerMovementChangeState()
    {
        _playerMovement.enabled = _playerMovementConditions;
        _playerMovementConditions = !_playerMovementConditions;
    }
    #endregion

    #region InputHandler
    public override void UnhidePlayer(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            StopHidingEvent?.Invoke();
            StopHidingHudEvent?.Invoke(false);
        }
    }
    #endregion
}
