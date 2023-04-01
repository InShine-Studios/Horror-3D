using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Astralization.Player
{
    public interface IPlayerMovement
    {
        float GetCurMoveSpeed();
        Camera GetMainCamera();
        GameObject GetRotatable();
        Vector3 GetMoveDirection();
        PlayerBase GetPlayerBase();
        bool IsSprinting();
        void SetFaceDirection(Vector2 moveInput);
        void SetSprinting(bool isSprinting);
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
        [SerializeField]
        [Tooltip("The PlayerBase for constants")]
        private PlayerBase _playerBase;
        [Tooltip("The camera that follows the player")]
        private Camera _mainCamera;
        [Tooltip("Rotating GameObjects of Player")]
        private GameObject _rotatable;
        [Tooltip("Camera Target for Facing Direction")]
        private GameObject _cameraTarget;
        [Tooltip("Animator of Player Model")]
        private PlayerAnimation _animation;

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
        [SerializeField]
        [Tooltip("Gravity Strength")]
        private float _gravity = -50.0f;
        [Tooltip("Force Grounding Flag")]
        [SerializeField]
        private bool _useForceGrounding = true;

        [Tooltip("True if player is sprinting")]
        private bool _isSprinting;
        [Tooltip("True if player is moving")]
        private bool _isMoving;
        #endregion

        #region SetGet
        public PlayerBase GetPlayerBase() { return _playerBase; }
        public float GetCurMoveSpeed() { return _curMoveSpeed; }
        public Vector3 GetMoveDirection() { return _moveDirection; }
        public bool IsSprinting() { return _isSprinting; }
        public void SetSprinting(bool isSprinting) { _isSprinting = isSprinting; }     //TODO Sprint with cooldown?
        public Camera GetMainCamera() { return _mainCamera; }
        public GameObject GetRotatable() { return _rotatable; }
        #endregion

        #region MonoBehaviour
        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _rotatable = transform.Find("Rotate").gameObject;
            _mainCamera = transform.parent.GetComponentInChildren<Camera>();
            _cameraTarget = transform.Find("CameraTarget").gameObject;
            _animation = GetComponentInChildren<PlayerAnimation>();
        }

        private void FixedUpdate()
        {
            SetDirection();
            _isMoving = MovePlayer();
            if (_isMoving) FindClosest?.Invoke();
            if (_useForceGrounding) ForceGrounding();
            SetAnimation();
        }
        #endregion

        #region MovementHandler
        private void SetDirection()
        {
            Vector3 direction = new Vector3(
                _cameraTarget.transform.position.x - _mainCamera.transform.position.x,
                0,
                _cameraTarget.transform.position.z - _mainCamera.transform.position.z
                ).normalized;
            //Debug.DrawRay(transform.position, direction);

            _moveAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            _moveDirection = Quaternion.Euler(0f, _moveAngle, 0f) * _faceDirection;

            if (_moveDirection.magnitude >= 0.1f)
            {
                float faceAngle = Mathf.Atan2(_moveDirection.x, _moveDirection.z) * Mathf.Rad2Deg;
                _rotatable.transform.rotation = Quaternion.Euler(0f, faceAngle, 0f);
                //Debug.DrawRay(transform.position, _rotatable.transform.rotation * Vector3.forward);
            }
        }

        public void SetFaceDirection(Vector2 moveInput)
        {
            _faceDirection = new Vector3(0, 0) { x = moveInput.x, z = moveInput.y }.normalized;
            //Debug.Log("[PLAYER MOVEMENT] Direction: " + _moveDirection);
        }

        private void ForceGrounding()
        {
            if (!_controller.isGrounded)
            {
                _controller.Move(new Vector3(0, _gravity, 0) * Time.deltaTime);
            }
        }

        private bool MovePlayer()
        {
            if (_isSprinting) _curMoveSpeed = _playerBase.GetSprintSpeed();
            else _curMoveSpeed = _playerBase.GetPlayerMovementSpeed();
            _controller.SimpleMove(_curMoveSpeed * Time.deltaTime * _moveDirection.normalized);
            return _faceDirection.magnitude != 0;
        }
        #endregion

        #region AnimationHandler
        private void SetAnimation()
        {
            _animation.SetMovementAnim(_isMoving, _isSprinting);
        }
        #endregion
    }
}