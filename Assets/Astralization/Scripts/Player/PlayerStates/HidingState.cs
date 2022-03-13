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
    [Tooltip("InteractableDetector script to get closets")]
    private InteractableDetector _interactableDetector;
    [Tooltip("Light object that gonna be toogled")]
    private Light _light;
    [Tooltip("Player previous position")]
    private Vector3 _prevPosition;
    #endregion

    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        _playerMovement = GetComponent<PlayerMovement>();
        _interactableDetector = GetComponentInChildren<InteractableDetector>();
        _light = this.transform.Find("Glow").GetComponent<Light>();
    }
    #endregion

    #region Handler
    public override void Enter()
    {
        base.Enter();
        PlayerMovementChangeState();
        _closets = _interactableDetector.GetClosest().transform.parent;
        _prevPosition = this.transform.position;
        Vector3 calOffset = _closets.GetComponent<Renderer>().bounds.center;
        this.transform.position = calOffset;
        _light.enabled = false;
    }

    public override void Exit()
    {
        base.Exit();
        this.transform.position = _prevPosition;
        _light.enabled = true;
        _closets = null;
        Invoke("PlayerMovementChangeState", 0.5f);
    }

    private void PlayerMovementChangeState()
    {
        _playerMovement.enabled = !_playerMovement.enabled;
    }

    public void SendStopHidingHudEvent()
    {
        StopHidingHudEvent?.Invoke(false);
    }

    public void SendStopHidingEvent()
    {
        StopHidingEvent?.Invoke();
    }
    #endregion

    #region InputHandler
    public override void UnhidePlayer(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            SendStopHidingHudEvent();
            StartCoroutine(Utils.DelayerHelper.Delay(1.0f, SendStopHidingEvent));
        }
    }
    #endregion
}
