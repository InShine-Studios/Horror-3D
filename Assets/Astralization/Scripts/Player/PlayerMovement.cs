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
    [SerializeField] [Tooltip("The camera that follows the player")]
    private Camera _mainCamera;
    [Tooltip("Rotating GameObjects of Player")]
    private GameObject _rotatable;

    [Space]
    [Header("Movement Constants")]
    [Tooltip("The movement speed used")]
    private float _curMoveSpeed;
    [Tooltip("The facing direction generated from keyboard")]
    private Vector3 _faceDirection = new Vector3(0, 0, 0);
    [Tooltip("The move direction calculated from move quaternion")]
    private Vector3 _moveDirection;
    [Tooltip("The move angle relative to camera and player position")]
    private float _moveAngle;
    [SerializeField] [Tooltip("Smoothing duration for turning")]
    private float turnSmoothTime = 0.1f;
    [Tooltip("Smoothing velocity for turning")]
    private float turnSmoothVelocity;
    [Tooltip("Gravity Strength")]
    public float Gravity = -50.0f;
    [Tooltip("Force Grounding Flag")] [SerializeField]
    private bool _useForceGrounding = true;

    [Tooltip("True if player is sprinting")]
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
        _rotatable = transform.Find("Rotate").gameObject;
    }

    private void FixedUpdate()
    {
        SetDirection();
        if (MovePlayer()) FindClosest?.Invoke();
        if (_useForceGrounding) ForceGrounding();
    }
    #endregion

    #region Handler
    private void SetDirection()
    {
        Vector3 direction = new Vector3(
            transform.position.x - _mainCamera.transform.position.x,
            0f,
            transform.position.z - _mainCamera.transform.position.z
        ).normalized;
        _moveAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, _moveAngle, ref turnSmoothVelocity, turnSmoothTime);
        _moveDirection = Quaternion.Euler(0f, _moveAngle, 0f) * _faceDirection;

        if (_faceDirection.magnitude >= 0.1f)
        {
            float faceAngle = Mathf.Atan2(_moveDirection.x, _moveDirection.z) * Mathf.Rad2Deg;
            _rotatable.transform.rotation = Quaternion.Euler(0f, faceAngle, 0f);
        }
    }

    public void GenerateMoveVector(Vector2 moveInput)
    {
        _faceDirection = new Vector3(0, 0) { x = moveInput.x, z = moveInput.y }.normalized;
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
        if (_faceDirection.magnitude >= 0.1f)
        {
            if (_isSprinting) _curMoveSpeed = _playerBase.GetSprintSpeed();
            else _curMoveSpeed = _playerBase.GetPlayerMovementSpeed();
            _controller.SimpleMove(_curMoveSpeed * Time.deltaTime * _moveDirection.normalized);
        }
        return _faceDirection.magnitude != 0;
    }
    #endregion
}
