using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHidingState : PlayerState
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
    [Tooltip("Player previous position")]
    private Vector3 _prevPosition;
    #endregion

    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        _playerMovement = GetComponent<PlayerMovement>();
        _interactableDetector = GetComponentInChildren<InteractableDetector>();
        //_playerCamera = this.transform.Find('Camera');
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
        Debug.Log(this.name);
        this.transform.position = new Vector3(calOffset.x, 0 , calOffset.z);
    }

    public override void Exit()
    {
        base.Exit();
        this.transform.position = _prevPosition;
        _closets = null;
        Invoke("PlayerMovementChangeState", 0.1f);
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
            owner.SetPlayerActionMap(Utils.PlayerHelper.States.Default);
        }
    }
    #endregion
}
