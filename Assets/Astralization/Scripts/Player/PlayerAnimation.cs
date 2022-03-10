using UnityEngine;

/*
 * Class to control player animation states.
 * Takes the current movement magnitude from PlayerMovement.
 * Takes the current rotation angle from PlayerRotation.
 */
public class PlayerAnimation : MonoBehaviour
{
    #region Variables
    [Header("Player Parts")]
    [Tooltip("The Animator for this player")]
    private Animator _animator;
    [SerializeField][Tooltip("The movement component for the speed")]
    private PlayerMovement _playerMovement;
    [SerializeField][Tooltip("The rotation component for the angle")]
    private PlayerRotation _playerRotation;
    [Tooltip("The camera that follows the player")]
    private Camera _mainCamera;
    [Tooltip("The rotating gameobjects of player")]
    private GameObject _rotatable;
    #endregion

    #region SetGet
    public void SetSpriteDir(Transform _rotatable, Transform _mainCamera)
    {
        Vector3 playerDir = _rotatable.rotation * Vector3.forward + _rotatable.position;
        Vector3 perspectiveDir = new Vector3(
            _mainCamera.position.x - playerDir.x,
            0f,
            _mainCamera.position.z - playerDir.z
            ).normalized;
        float currentAngle = Mathf.Atan2(perspectiveDir.x, perspectiveDir.z) * Mathf.Rad2Deg;
        currentAngle = Utils.MathCalcu.mod((int)currentAngle, 360);

        if (currentAngle >= 135 && currentAngle < 225)
        {
            _animator.SetInteger("Direction", (int)Utils.PlayerHelper.Direction.Up);
        }
        else if (currentAngle >= 45 && currentAngle < 135)
        {
            _animator.SetInteger("Direction", (int)Utils.PlayerHelper.Direction.Right);
        }
        else if (currentAngle >= 315 || currentAngle < 45)
        {
            _animator.SetInteger("Direction", (int)Utils.PlayerHelper.Direction.Down);
        }
        else if (currentAngle >= 225 && currentAngle < 315)
        {
            _animator.SetInteger("Direction", (int)Utils.PlayerHelper.Direction.Left);
        }
    }

    private void SetPlayerMoveAnim()
    {
        _animator.SetBool("IsIdle", false);
        _animator.SetBool("IsWalking", false);
        _animator.SetBool("IsSprinting", false);

        if (_playerMovement.GetMoveDirection().magnitude == 0)
        {
            _animator.SetBool("IsIdle", true);
        }
        else if (_playerMovement.IsSprinting())
        {
            _animator.SetBool("IsSprinting", true);
        }
        else
        {
            _animator.SetBool("IsWalking", true);
        }
    }
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void Start()
    {
        PlayerMovement playerMovement = GetComponentInParent<PlayerMovement>();
        _mainCamera = playerMovement.GetMainCamera();
        _rotatable = playerMovement.GetRotatable();
    }

    private void Update()
    {
        SetPlayerMoveAnim();
    }
    #endregion
}
