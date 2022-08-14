using System;
using UnityEngine;
using UnityEngine.InputSystem;
using ElRaccoone.Tweens;

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
    private GameObject _playerLookAt;
    private GameObject _playerTarget;
    private GameObject _closetsPoint;
    private Cinemachine.CinemachineFreeLook _freelook;
    #endregion

    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        _playerMovement = GetComponent<PlayerMovement>();
        _interactableDetector = GetComponentInChildren<InteractableDetector>();
        _freelook = this.transform.parent.GetComponentInChildren<Cinemachine.CinemachineFreeLook>();
        _playerLookAt = GameObject.Find("Character/CameraLookAt");
        _playerTarget = GameObject.Find("Character/CameraTarget");
        _closetsPoint = GameObject.Find("Closets/CameraPointTo");
    }
    #endregion

    #region StateHandler
    public override void Enter()
    {
        base.Enter();
        PlayerMovementChangeState();
        _closets = _interactableDetector.GetClosest().transform.parent;
        Debug.Log(_closets.name);
        _prevPosition = this.transform.position;
        Debug.Log(_freelook.name);
        Vector3 calOffset = _closets.GetComponentInChildren<Renderer>().bounds.center;
        this.transform.position = new Vector3(calOffset.x, 0 , calOffset.z);
        _freelook.m_LookAt = _closetsPoint.transform;
        _freelook.m_Follow = _closetsPoint.transform;
    }

    public override void Exit()
    {
        base.Exit();
        this.transform.position = _prevPosition;
        _freelook.m_LookAt = _playerLookAt.transform;
        _freelook.m_Follow = _playerTarget.transform;
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
