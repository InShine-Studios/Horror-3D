using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class HidingState : PlayerState
{
    #region Events
    public static event Action<bool> StopHidingHudEvent;
    #endregion

    #region Variables
    [Tooltip("Closets interactable that been interacted by player")]
    private Transform _closets;
    [Tooltip("PlayerMovement script that gonna be toogled")]
    private PlayerMovement _playerMovement;
    [Tooltip("InteractableDetector script to get closets")]
    private InteractableDetector _interactableDetector;
    [Tooltip("Light object that gonna be toogled")]
    private Light _light;
    [Tooltip("Player previous position")]
    private Vector3 _prevPosition;

    [Tooltip("True if player is able to unhide. Prevent spamming on hiding")]
    private bool _enableUnhide = false;
    #endregion

    #region SetGet
    private void EnableUnhiding()
    {
        _enableUnhide = true;
    }
    #endregion

    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        _enableUnhide = false;
        _playerMovement = GetComponent<PlayerMovement>();
        _interactableDetector = GetComponentInChildren<InteractableDetector>();
        _light = this.transform.Find("Glow").GetComponent<Light>();

        HidingOverlay.FinishHiding += EnableUnhiding;
        HidingOverlay.FinishUnhiding += ChangeToDefaultState;
    }
    #endregion

    #region StateHandler
    public override void Enter()
    {
        base.Enter();
        PlayerMovementChangeState();
        _closets = _interactableDetector.GetClosest().transform.parent;
        _prevPosition = this.transform.position;
        Vector3 calOffset = _closets.GetComponentInChildren<Renderer>().bounds.center;
        this.transform.position = calOffset;
        _light.enabled = false;
    }

    public override void Exit()
    {
        base.Exit();
        this.transform.position = _prevPosition;
        _light.enabled = true;
        _closets = null;
        Invoke("PlayerMovementChangeState", 0.1f);
    }

    private void PlayerMovementChangeState()
    {
        _playerMovement.enabled = !_playerMovement.enabled;
    }

    private void ChangeToDefaultState()
    {
        owner.SetPlayerActionMap(Utils.PlayerHelper.States.Default);
    }
    #endregion

    #region InputHandler
    public override void UnhidePlayer(InputAction.CallbackContext ctx)
    {
        if (ctx.performed & _enableUnhide)
        {
            StopHidingHudEvent?.Invoke(false);
        }
    }
    #endregion
}
