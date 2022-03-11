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
    private Transform _closets;
    [Tooltip("PlayerMovement script that gonna be toogled")]
    private PlayerMovement _playerMovement;
    [Tooltip("Player previous position")]
    private Vector3 _prevPosition;
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
        _closets = GetComponentInChildren<InteractableDetector>().GetClosest().transform.parent;
        _prevPosition = this.transform.position;
        Vector3 calOffset = _closets.GetComponent<Renderer>().bounds.center;
        this.transform.position = calOffset;
        this.transform.Find("Glow").GetComponent<Light>().enabled = false;
    }

    public override void Exit()
    {
        base.Exit();
        this.transform.position = _prevPosition;
        this.transform.Find("Glow").GetComponent<Light>().enabled = true;
        _closets = null;
        Invoke("PlayerMovementChangeState", 1.0f);
    }

    private void PlayerMovementChangeState()
    {
        _playerMovement.enabled = !_playerMovement.enabled;
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
