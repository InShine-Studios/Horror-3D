using System;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IPlayerMovement
{
    PlayerBase GetPlayerBase();
    Vector3 GetMoveDirection();
    bool IsSprinting();
    float GetCurMoveSpeed();
    void GenerateMoveVector(Vector2 moveInput);
}

/*
 * Class to control the player movement.
 * Using Input Actions.
 */
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour, IPlayerMovement
{
    #region Events
    public static event Action FindClosest;
    #endregion

    #region Variables
    [Header("External Variables")]
    [Tooltip("The Controller component")]
    private CharacterController _controller;
    [SerializeField] [Tooltip("The PlayerBase for constants")]
    private PlayerBase _playerBase;

    [Space]
    [Header("Movement Constants")]
    [Tooltip("The movement speed used")]
    private float _curMoveSpeed;
    [Tooltip("The move direction generated")]
    private Vector3 _moveDirection;
    [Tooltip("Gravity Strength")]
    public float Gravity = -50.0f;
    [Tooltip("Force Grounding Flag")] [SerializeField]
    private bool _useForceGrounding = true;

    [Tooltip("Sprinting boolean")]
    private bool _isSprinting;
    #endregion

    #region SetGet
    public PlayerBase GetPlayerBase() { return _playerBase; }
    public float GetCurMoveSpeed() { return _curMoveSpeed; }
    public Vector3 GetMoveDirection() { return _moveDirection; }
    public bool IsSprinting() { return _isSprinting; }
    public void SetSprinting(bool isSprinting) { _isSprinting = isSprinting; }     //TODO Sprint with cooldown?
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        if (MovePlayer()) FindClosest?.Invoke();
        if (_useForceGrounding) ForceGrounding();
    }
    #endregion

    #region Handler
    public void GenerateMoveVector(Vector2 moveInput)
    {
        _moveDirection = new Vector3(0, 0) { x = moveInput.x, z = moveInput.y };
        //Debug.Log("[PLAYER MOVEMENT] Direction: " + _moveDirection);
    }

    private void ForceGrounding()
    {
        if (!_controller.isGrounded)
        {
            _controller.Move(new Vector3(0, Gravity, 0) * Time.deltaTime);
        }
    }

    private bool MovePlayer()
    {
        if (_isSprinting) _curMoveSpeed = _playerBase.GetSprintSpeed();
        else _curMoveSpeed = _playerBase.GetPlayerMovementSpeed();
        _controller.SimpleMove(_curMoveSpeed * Time.deltaTime * _moveDirection);
        return _moveDirection.magnitude != 0;
    }
    #endregion
}
