using System;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IPlayerMovement
{
    PlayerBase GetPlayerBase();
    Vector3 GetMoveDirection();
    bool GetSprintBool();
    float GetCurMoveSpeed();
    void OnMovementInput(InputAction.CallbackContext inputVal);
    void SprintPressed(InputAction.CallbackContext ctx);
    void SprintReleased(InputAction.CallbackContext ctx);
}

/*
 * Class to control the player movement.
 * Using Input Actions.
 */
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour, IPlayerMovement
{
    public static event Action FindClosest;

    #region Movement Variables
    [Header("External Variables")]
    [Tooltip("The Controller component")]
    private CharacterController _controller;
    [SerializeField] [Tooltip("The PlayerBase for constants")]
    private PlayerBase _playerBase;

    [Space]
    [Header("Movement Constants")]
    [Tooltip("The movement speed used")]
    private float _curMoveSpeed;
    [Tooltip("The movement input from input actions")]
    private Vector2 _moveInput = new Vector2(0, 0);
    [Tooltip("The move direction generated")]
    private Vector3 _moveDirection;
    [Tooltip("Gravity Strength")]
    public float Gravity = -50.0f;
    [Tooltip("Force Grounding Flag")] [SerializeField]
    private bool _useForceGrounding = true;

    [Tooltip("Sprinting boolean")]
    private bool _isSprinting;
    #endregion

    public PlayerBase GetPlayerBase() { return _playerBase; }
    public float GetCurMoveSpeed() { return _curMoveSpeed; }
    public Vector3 GetMoveDirection() { return _moveDirection; }
    public bool GetSprintBool(){ return _isSprinting; }

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        if (MovePlayer()) FindClosest?.Invoke();
        if (_useForceGrounding) ForceGrounding();
    }


    #region Input System
    // Read movement input and set move direction
    public void OnMovementInput(InputAction.CallbackContext inputVal)
    {
        _moveInput = inputVal.ReadValue<Vector2>();
        _moveDirection = new Vector3(0, 0) { x = _moveInput.x, z = _moveInput.y };
        //Debug.Log("[PLAYER] Movement direction: " + moveDirection);
    }

    //TODO Sprint with cooldown?
    public void SprintPressed(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _isSprinting = true;
            //Debug.Log(this.name + " started sprinting " + isSprinting);
        }
    }

    public void SprintReleased(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _isSprinting = false;
            //Debug.Log(this.name + " no longer sprinting " + isSprinting);
        }
    }
    #endregion

    private void ForceGrounding()
    {
        if (!_controller.isGrounded)
        {
            _controller.Move(new Vector3(0, Gravity, 0) * Time.deltaTime);
        }
    }

    private bool MovePlayer()
    {
        _curMoveSpeed = _playerBase.GetPlayerMovementSpeed();
        if (_isSprinting)
            _curMoveSpeed = _playerBase.GetSprintSpeed();
        _controller.SimpleMove(_curMoveSpeed * Time.deltaTime * _moveDirection);
        return _moveDirection.magnitude != 0;
    }
}
