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
    private float _prevTopRigRadius;
    private float _prevBotRigRadius;
    private float _prevMidRigRadius;
    private GameObject _playerLookAt;
    private GameObject _playerTarget;
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
    }
    #endregion

    #region StateHandler
    public override void Enter()
    {
        base.Enter();
        PlayerMovementChangeState();
        _closets = _interactableDetector.GetClosest().transform.parent;
        _prevPosition = this.transform.position;
        Debug.Log(_freelook.name);
        Vector3 calOffset = _closets.GetComponentInChildren<Renderer>().bounds.center;
        this.transform.position = new Vector3(calOffset.x, 0 , calOffset.z);
        _prevTopRigRadius = _freelook.m_Orbits[0].m_Radius;
        _prevBotRigRadius = _freelook.m_Orbits[1].m_Radius;
        _prevMidRigRadius = _freelook.m_Orbits[2].m_Radius;
        _freelook.m_LookAt = _closets;
        _freelook.m_Follow = _closets;
        _freelook.m_Orbits[0].m_Radius = 5;
        _freelook.m_Orbits[1].m_Radius = 5;
        _freelook.m_Orbits[2].m_Radius = 5;
    }

    public override void Exit()
    {
        base.Exit();
        this.transform.position = _prevPosition;
        _freelook.m_LookAt = _playerLookAt.transform;
        _freelook.m_Follow = _playerTarget.transform;
        _freelook.m_Orbits[0].m_Radius = _prevTopRigRadius;
        _freelook.m_Orbits[1].m_Radius = _prevBotRigRadius;
        _freelook.m_Orbits[2].m_Radius = _prevMidRigRadius;
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
