using UnityEngine;

/*
 * Class to control player animation states.
 * Takes the current movement magnitude from PlayerMovement.
 * Takes the current rotation angle from PlayerRotation.
 */
public class PlayerSpriteAnim : MonoBehaviour
{
    #region Variables
    [Header("Player Parts")]
    [Tooltip("The Animator for this player")]
    private Animator _animator;
    #endregion

    #region SetGet
    public void SetSpriteIdleDirection(Transform _rotatable, Transform _mainCamera)
    {
        float perspectiveAngle = _mainCamera.rotation.y - _rotatable.rotation.y;
        perspectiveAngle = Utils.MathCalcu.mod((int)(perspectiveAngle*180), 360);

        if (perspectiveAngle >= 135 && perspectiveAngle < 225)
        {
            _animator.SetInteger("Direction", (int)Utils.PlayerHelper.Direction.Down);
        }
        else if (perspectiveAngle >= 45 && perspectiveAngle < 135)
        {
            _animator.SetInteger("Direction", (int)Utils.PlayerHelper.Direction.Left);
        }
        else if (perspectiveAngle >= 315 || perspectiveAngle < 45)
        {
            _animator.SetInteger("Direction", (int)Utils.PlayerHelper.Direction.Up);
        }
        else if (perspectiveAngle >= 225 && perspectiveAngle < 315)
        {
            _animator.SetInteger("Direction", (int)Utils.PlayerHelper.Direction.Right);
        }
    }

    public void SetSpriteMovingDirection(Vector3 faceDirection)
    {
        if (faceDirection.x > 0)
        {
            _animator.SetInteger("Direction", (int)Utils.PlayerHelper.Direction.Right);
        }
        else if (faceDirection.x < 0)
        {
            _animator.SetInteger("Direction", (int)Utils.PlayerHelper.Direction.Left);
        }
        else if (faceDirection.z > 0)
        {
            _animator.SetInteger("Direction", (int)Utils.PlayerHelper.Direction.Up);
        }
        else if (faceDirection.z < 0)
        {
            _animator.SetInteger("Direction", (int)Utils.PlayerHelper.Direction.Down);
        }
    }

    public void SetPlayerMoveAnim(bool isMoving, bool isSprinting)
    {
        _animator.SetBool("IsIdle", false);
        _animator.SetBool("IsWalking", false);
        _animator.SetBool("IsSprinting", false);

        if (!isMoving)
        {
            _animator.SetBool("IsIdle", true);
        }
        else if (isSprinting)
        {
            _animator.SetBool("IsSprinting", true);
        }
        else
        {
            _animator.SetBool("IsWalking", true);
        }
    }

    public void SetAnimState(string animName)
    {
        _animator.Play(animName);
    }
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    #endregion
}
